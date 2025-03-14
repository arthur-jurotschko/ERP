import { Component, OnInit, AfterViewInit, ViewChildren, ElementRef, ViewContainerRef } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControlName } from '@angular/forms';
import { Router } from '@angular/router';

import { CustomValidators } from 'ng2-validation'
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/fromEvent';
import 'rxjs/add/observable/merge';


import { GenericValidator } from '../../common/validation/generic-form-validator';
import { Usuario } from '../models/usuario';
import { UsuarioService } from '../services/usuario.service';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit, AfterViewInit {

  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];
  
  loginForm: FormGroup;
  private usuario: Usuario;
  private validationMessages: { [key: string]: { [key: string]: string } };
  private genericValidator: GenericValidator;
  displayMessage: { [key: string]: string } = {};
  public errors: any[] = [];
  private token: string;

  constructor(private fb: FormBuilder, 
    private usuarioService: UsuarioService,
    private router: Router,
    private toastr: ToastrService ) {

      
      this.validationMessages = {
        email: {
          required: 'Informe o e-mail',
          email: 'Email invalido'
        },
        senha: {
          required: 'Informe a senha',
          minlength: 'A senha deve possuir no mínimo 6 caracteres'
        },
      };

      this.genericValidator = new GenericValidator(this.validationMessages);
    }
    

  ngOnInit() {
    
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, CustomValidators.email]],
      senha: ['', [Validators.required, Validators.minLength(6)]]
      
    });
  }

  ngAfterViewInit(): void {
    let controlBlurs: Observable<any>[] = this.formInputElements
    .map((formControl: ElementRef) => Observable.fromEvent(formControl.nativeElement, 'blur'));
    Observable.merge(...controlBlurs).subscribe(value => {
      this.displayMessage = this.genericValidator.processMessages(this.loginForm);
    });
  }

  realizarLogin() {
    //Mandar o organizador com suas propriedades para ser consumido pelo servico REST da nossa API.
    //inscricaoForm mapeia organizador que se comunica com o REST - API.
    if (this.loginForm.valid && this.loginForm.dirty) {
      let u = Object.assign({}, this.usuario, this.loginForm.value);
      this.usuarioService.login(u)
        .subscribe(
          result => { this.onSaveComplete(result) },
          fail => { this.onError(fail) }
        );
    }
  }

  onSaveComplete(response: any) {
    this.errors = [];
    this.loginForm.reset();
 
 localStorage.setItem('amj.token', response.result.access_token);
 localStorage.setItem('amj.user', JSON.stringify(response.result.user));
 
 this.toastr.success('Inscrição realizada com sucesso','Obrigado!',{
  timeOut: 3000 });
  this.router.navigate(['/clientes/meus-clientes']);
}
 
   onError(fail) {
    this.toastr.error('Ocorreu um erro!','Opa :(');
    this.errors = fail.error.errors;
    
  }
 
}
