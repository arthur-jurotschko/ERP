import { Component, OnInit, AfterViewInit, ViewChildren, ElementRef, ViewContainerRef } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControlName, FormControl } from '@angular/forms';
import { Router } from '@angular/router';

import { CustomValidators } from 'ng2-validation';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/fromEvent';
import 'rxjs/add/observable/merge';

import { GenericValidator } from '../../common/validation/generic-form-validator';
import { ToastrService } from 'ngx-toastr';
import { UsuarioService } from '../services/usuario.service';
import { Usuario } from '../models/usuario';

@Component({
  selector: 'app-confirmacaoemail',
  templateUrl: './confirmacaoemail.component.html',
  styleUrls: ['./confirmacaoemail.component.scss']
})
export class ConfirmacaoemailComponent implements OnInit, AfterViewInit {

  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];
  
  confirmacaoemailForm: FormGroup;
  private usuario: Usuario;
  private validationMessages: { [key: string]: { [key: string]: string } };
  private genericValidator: GenericValidator;
  displayMessage: { [key: string]: string } = {};
  public errors: any[] = [];

  constructor(private fb: FormBuilder, 
    private usuarioService: UsuarioService,
    private router: Router,
    private toastr: ToastrService  ) {

      
      this.validationMessages = {
        email: {
          required: 'Informe o e-mail',
          email: 'Email invalido'
        },
    
      };

      this.genericValidator = new GenericValidator(this.validationMessages);
    }
     

  ngOnInit() {
    
    this.confirmacaoemailForm = this.fb.group({
      email: ['', [Validators.required, CustomValidators.email]]
      
    });
  }

  ngAfterViewInit(): void {
    let controlBlurs: Observable<any>[] = this.formInputElements
    .map((formControl: ElementRef) => Observable.fromEvent(formControl.nativeElement, 'blur'));
    Observable.merge(...controlBlurs).subscribe(value => {
      this.displayMessage = this.genericValidator.processMessages(this.confirmacaoemailForm);
    });
  }
  
      confirmacaoEmail() {
        //Mandar o organizador com suas propriedades para ser consumido pelo servico REST da nossa API.
        //inscricaoForm mapeia organizador que se comunica com o REST - API.
        if (this.confirmacaoemailForm.valid && this.confirmacaoemailForm.dirty) {
          let u = Object.assign({}, this.usuario, this.confirmacaoemailForm.value);
          this.usuarioService.ConfirmacaoEmail(u)
            .subscribe(
              result => { this.onSaveComplete(result) },
              fail => { this.onError(fail) }
            );
        }
      }
  
      onSaveComplete(response: any) {
        this.errors = [];
        this.confirmacaoemailForm.reset();
    
     
        this.toastr.success('Inscrição realizada com sucesso','Obrigado!',{
          timeOut: 3000 });
          this.router.navigate(['/ligacao/minhas-ligacoes']);
        }
         
     
       onError(fail) {
        this.toastr.error('Ocorreu um erro!','Opa :(');
        this.errors = fail.error.errors;
        
      }
     
    }
  
  