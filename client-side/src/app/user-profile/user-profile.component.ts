import { Component, OnInit, AfterViewInit, ViewChildren, ElementRef, ViewContainerRef } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControlName, FormControl } from '@angular/forms';
import { Router } from '@angular/router';

import { CustomValidators } from 'ng2-validation';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/fromEvent';
import 'rxjs/add/observable/merge';



import { SeoService, SeoModel } from 'app/services/seo.services';
import { Usuario } from './models/usuario';
import { GenericValidator } from 'app/common/validation/generic-form-validator';
import { UsuarioService } from './services/usuario.service';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit, AfterViewInit {

  teste: string = "";
  inscricaoForm: FormGroup;
  private usuario: Usuario;
  private validationMessages: { [key: string]: { [key: string]: string } };
  private genericValidator: GenericValidator;
  displayMessage: { [key: string]: string } = {};
  public errors: any[] = [];


  constructor(private fb: FormBuilder, 
    private usuarioService: UsuarioService,
    private router: Router,
    seoService: SeoService,
    private toastr: ToastrService) {


    this.validationMessages = {
      nome: {
        required: 'O Nome é requerido.',
        minlength: 'O Nome precisa ter no mínimo 2 caracteres',
        maxlength: 'O Nome precisa ter no máximo 15 caracteres'
      },
      cpf: {
        required: 'Informe o CPF',
        rangeLength: 'CPF deve conter 11 caracteres'
      },
      email: {
        required: 'Informe o e-mail',
        email: 'Email invalido'
      },
      senha: {
        required: 'Informe a senha',
        minlength: 'A senha deve possuir no mínimo 6 caracteres'
      },
      senhaConfirmacao: {
        required: 'Informe a senha novamente',
        minlength: 'A senha deve possuir no mínimo 6 caracteres',
        equalTo: 'As senhas não conferem'
      }
    };
   
    this.genericValidator = new GenericValidator(this.validationMessages);
    
    let seoModel: SeoModel = <SeoModel>{
      title: 'Registre-se',
      description: 'Lista dos próximos eventos técnicos no Brasil',
      robots: 'Index, Follow',
      keywords: 'eventos,workshops,encontros,congressos'
    };

    seoService.setSeoData(seoModel);
  }

  ngOnInit() {
//Validar se Senha é igual a SenhaConfirmacao
    let senha = new FormControl('', [Validators.required, Validators.minLength(6)]);
    let senhaConfirmacao = new FormControl('', [Validators.required, Validators.minLength(6), CustomValidators.equalTo(senha)]);

    this.inscricaoForm = this.fb.group({
      nome: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(15)]],
      cpf: ['', [Validators.required, CustomValidators.rangeLength([11,11])]],
      email: ['', [Validators.required, CustomValidators.email]],
      senha: senha,
      senhaConfirmacao: senhaConfirmacao
    });
  }
  
  //Configuração Observable
  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];
  ngAfterViewInit(): void {
    let controlBlurs: Observable<any>[] = this.formInputElements
    .map((formControl: ElementRef) => Observable.fromEvent(formControl.nativeElement, 'blur'));
    Observable.merge(...controlBlurs).subscribe(value => {
      this.displayMessage = this.genericValidator.processMessages(this.inscricaoForm);
    });
  }

  adicionarUsuario() {
    //Mandar o organizador com suas propriedades para ser consumido pelo servico REST da nossa API.
    //inscricaoForm mapeia organizador que se comunica com o REST - API.
    if (this.inscricaoForm.valid && this.inscricaoForm.dirty) {
      let u = Object.assign({}, this.usuario, this.inscricaoForm.value);
     this.usuarioService.registrarUsuario(u)
       .subscribe(
          result => { this.onSaveComplete(result) },
          fail => { this.onError(fail) }
        );
    }
  }

  onSaveComplete(response: any) {
   this.errors = [];
   this.inscricaoForm.reset();

localStorage.setItem('amj.token', response.result.access_token);
localStorage.setItem('amj.user', JSON.stringify(response.result.user));


this.toastr.success('Inscrição realizada com sucesso','Obrigado!',{
  timeOut: 3000 });
  this.router.navigate(['/ligacao/minhas-ligacoes']);
}
  
  onError(fail) {
    this.toastr.error('Ocorreu um erro!','Opa :(');
    this.errors = fail.error.errors;
   
  }

  

}
