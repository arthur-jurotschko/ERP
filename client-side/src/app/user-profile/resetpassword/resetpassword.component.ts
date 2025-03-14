import { Component, OnInit, AfterViewInit, ElementRef, ViewChildren } from '@angular/core';
import { FormControlName, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Usuario } from '../models/usuario';
import { GenericValidator } from 'app/common/validation/generic-form-validator';
import { UsuarioService } from '../services/usuario.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CustomValidators } from 'ng2-validation';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-resetpassword',
  templateUrl: './resetpassword.component.html',
  styleUrls: ['./resetpassword.component.scss']
})
export class ResetpasswordComponent implements OnInit, AfterViewInit {

  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];
  
  forgotpasswordForm: FormGroup;
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
    
    this.forgotpasswordForm = this.fb.group({
      email: ['', [Validators.required, CustomValidators.email]]
      
    });
  }

  ngAfterViewInit(): void {
    let controlBlurs: Observable<any>[] = this.formInputElements
    .map((formControl: ElementRef) => Observable.fromEvent(formControl.nativeElement, 'blur'));
    Observable.merge(...controlBlurs).subscribe(value => {
      this.displayMessage = this.genericValidator.processMessages(this.forgotpasswordForm);
    });
  }
  
      forgotPassword() {
        //Mandar o organizador com suas propriedades para ser consumido pelo servico REST da nossa API.
        //inscricaoForm mapeia organizador que se comunica com o REST - API.
        if (this.forgotpasswordForm.valid && this.forgotpasswordForm.dirty) {
          let u = Object.assign({}, this.usuario, this.forgotpasswordForm.value);
          this.usuarioService.ForgotPassword(u)
            .subscribe(
              result => { this.onSaveComplete(result) },
              fail => { this.onError(fail) }
            );
        }
      }
  
      onSaveComplete(response: any) {
        this.errors = [];
        this.forgotpasswordForm.reset();
    
     
        this.toastr.success('E-mail de redefinição de senha enviado com sucesso','Sucesso',{
          timeOut: 3000 });
          this.router.navigate(['/forgotpassword']);
        }
     
       onError(fail) {
        this.toastr.error('Ocorreu um erro','Erro');
        this.errors = fail.error.errors;
        
      }
     
    }

