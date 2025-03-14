import { Component, OnInit, AfterViewInit, ViewChildren, ElementRef, ViewContainerRef } from '@angular/core';
import { DateUtils } from 'app/common/data-type-utils/date-utils';
import { FormGroup, FormBuilder, Validators, FormControlName, FormControl } from '@angular/forms';
import { Cliente, Genero, Endereco } from '../models/cliente';
import { GenericValidator } from 'app/common/validation/generic-form-validator';
import { ClientesService } from '../services/clientes.services';
import { Router } from '@angular/router';
import { SeoService, SeoModel } from 'app/services/seo.services';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/fromEvent';
import 'rxjs/add/observable/merge';
//import { CurrencyUtils } from 'app/common/data-type-utils/currency-utils';

@Component({
  selector: 'app-adicionar-clientes',
  templateUrl: './adicionar-clientes.component.html',
  styleUrls: ['./adicionar-clientes.component.scss']
})
export class AdicionarClientesComponent implements OnInit, AfterViewInit {
  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];
  public myDatePickerOptions = DateUtils.getMyDatePickerOptions();

  public errors: any[] = [];
  public clienteForm: FormGroup;
  public cliente: Cliente;
  public generos: Genero[];

  public checkendereco: boolean;

  constructor(private fb: FormBuilder,
    private clienteService: ClientesService,
    private router: Router,
    private toastr: ToastrService,
    seoService: SeoService) {

    this.validationMessages = {
      nomecompleto: {
        required: 'O Nome é requerido.',
        minlength: 'O Nome precisa ter no mínimo 2 caracteres',
        maxlength: 'O Nome precisa ter no máximo 150 caracteres'
      },
      cpf: {
        required: 'O Nome é requerido.',
        minlength: 'O Nome precisa ter no mínimo 2 caracteres',
        maxlength: 'O Nome precisa ter no máximo 150 caracteres'
      },
      rg: {
        required: 'O Nome é requerido.',
        minlength: 'O Nome precisa ter no mínimo 2 caracteres',
        maxlength: 'O Nome precisa ter no máximo 150 caracteres'
      },
      telresidencial: {
        required: 'O Nome é requerido.',
        minlength: 'O Nome precisa ter no mínimo 2 caracteres',
        maxlength: 'O Nome precisa ter no máximo 150 caracteres'
      },
      telcelular: {
        required: 'O Nome é requerido.',
        minlength: 'O Nome precisa ter no mínimo 2 caracteres',
        maxlength: 'O Nome precisa ter no máximo 150 caracteres'
      },
      email: {
        required: 'O Nome é requerido.',
        minlength: 'O Nome precisa ter no mínimo 2 caracteres',
        maxlength: 'O Nome precisa ter no máximo 150 caracteres'
      },
      redesocial: {
        required: 'O Nome é requerido.',
        minlength: 'O Nome precisa ter no mínimo 2 caracteres',
        maxlength: 'O Nome precisa ter no máximo 150 caracteres'
      },
      data: {
        required: 'Informe a data de início'
      },
      generoId: {
        required: 'Informe o genero'
      }
    };

    this.genericValidator = new GenericValidator(this.validationMessages);
    this.cliente = new Cliente();
    this.cliente.endereco = new Endereco();

    let seoModel: SeoModel = <SeoModel>{
      title: 'Cadastre-se',
      description: 'Lista dos próximos eventos técnicos no Brasil',
      robots: 'Index, Follow',
      keywords: 'eventos,workshops,encontros,congressos'
    };

    seoService.setSeoData(seoModel);

  }

  displayMessage: { [key: string]: string } = {};
  private validationMessages: { [key: string]: { [key: string]: string } };
  private genericValidator: GenericValidator;

  ngOnInit(): void {
    this.clienteForm = this.fb.group({
      nomecompleto: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(150)]],
      cpf: '',
      rg: '',
      generoId: ['',[Validators.required]],
      telresidencial: '',
      telcelular: '',
      email: '',
      redesocial: '',
      data: ['',[Validators.required]],
      checkendereco: '',
      logradouro: '',
      numero: '',
      complemento: '',
      bairro: '',
      cep: '',
      cidade: '',
      estado: '',
    });

    this.clienteService.obterGeneros()
      .subscribe(cat => this.generos = cat, error => this.errors = error);
  }

  ngAfterViewInit(): void {
    let controlBlurs: Observable<any>[] = this.formInputElements
      .map((formControl: ElementRef) => Observable.fromEvent(formControl.nativeElement, 'blur'));

    Observable.merge(...controlBlurs).subscribe(value => {
      this.displayMessage = this.genericValidator.processMessages(this.clienteForm);
    });
  }

  adicionarCliente() {
    if (this.clienteForm.dirty && this.clienteForm.valid) {
      let user = this.clienteService.obterUsuario();

      let p = Object.assign({}, this.cliente, this.clienteForm.value);
      p.masterUserId = user.id;

      p.nomecompleto = p.nomecompleto;
      p.cpf = p.cpf;
      p.rg = p.rg;
      p.telresidencial = p.telresidencial;
      p.telcelular = p.telcelular;
      p.email = p.email;
      p.redesocial = p.redesocial;
      p.data = DateUtils.getMyDatePickerDate(p.data);

      p.endereco.logradouro = p.logradouro;
      p.endereco.numero = p.numero;
      p.endereco.complemento = p.complemento;
      p.endereco.bairro = p.bairro;
      p.endereco.cep = p.cep;
      p.endereco.cidade = p.cidade;
      p.endereco.estado = p.estado;

      if (this.clienteForm.valid) {
        this.clienteService.registrarClientes(p)
          .subscribe(
            result => { this.onSaveComplete() },
            fail => { this.onError(fail); }
          );
      }

      if (this.clienteForm.valid) {
        this.clienteService.adicionarClienteDashboard()
          .subscribe(
            result => { this.onSaveComplete() },
            fail => { this.onError(fail); }
          );
      }

    }
  }



  onError(fail) {
    this.toastr.error('Ocorreu um erro no processamento', 'Ops! :(');
    this.errors = fail.error.errors;
  }

  onSaveComplete() {

    this.clienteForm.reset();
    this.errors = [];

    this.toastr.success('Inscrição realizada com sucesso', 'Obrigado!', {
      timeOut: 3000
    });
    this.router.navigate(['/clientes/meus-clientes']);
  }

}
