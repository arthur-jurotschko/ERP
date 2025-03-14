import { Component, OnInit, ViewChildren, AfterViewInit, ElementRef } from '@angular/core';
import { FormControlName, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { DateUtils } from 'app/common/data-type-utils/date-utils';
import { Cliente, Endereco, Genero } from '../models/cliente';
import { Observable } from 'rxjs/Observable';
import { Subscription } from 'rxjs/Subscription';
import 'rxjs/add/operator/debounceTime';
import 'rxjs/add/observable/fromEvent';
import 'rxjs/add/observable/merge';
import { ClientesService } from '../services/clientes.services';
import { Router, ActivatedRoute } from '@angular/router';
import { GenericValidator } from 'app/common/validation/generic-form-validator';
import { SeoService, SeoModel } from 'app/services/seo.services';
import { ToastrService } from 'ngx-toastr';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-editar-clientes',
  templateUrl: './editar-clientes.component.html',
  styleUrls: ['./editar-clientes.component.scss']
})

export class EditarClientesComponent implements OnInit, AfterViewInit {
  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

  public myDatePickerOptions = DateUtils.getMyDatePickerOptions();

  public errors: any[] = [];
  public errorsEndereco: any[] = [];

  public clienteForm: FormGroup;
  public enderecoForm: FormGroup;

  public cliente: Cliente;
  public generos: Genero[];
  public endereco: Endereco;

  public clienteId: string = "";

  public checkEndereco: boolean;
  public sub: Subscription;
  public modalVisible: boolean;
  public enderecoMap: any;

  constructor(private fb: FormBuilder,
    private clienteService: ClientesService,
    private router: Router,
    seoService: SeoService,
    private routeAc: ActivatedRoute,
    private toastr: ToastrService,
    private sanitizer: DomSanitizer) {


    this.validationMessages = {
      nomecompleto: {
        required: 'Informe a senha',
        minlength: 'A senha deve possuir no mínimo 6 caracteres'
      },


    };

    this.genericValidator = new GenericValidator(this.validationMessages);
    this.cliente = new Cliente();
    this.cliente.endereco = new Endereco();
    this.modalVisible = false;


    let seoModel: SeoModel = <SeoModel>{
      title: 'Cadastre-se',
      description: 'Lista dos próximos clientes técnicos no Brasil',
      robots: 'Index, Follow',
      keywords: 'clientes,workshops,encontros,congressos'
    };

    seoService.setSeoData(seoModel);

  }

  displayMessage: { [key: string]: string } = {};
  private validationMessages: { [key: string]: { [key: string]: string } };
  private genericValidator: GenericValidator;

  ngOnInit() {
    this.clienteForm = this.fb.group({
      nomeCompleto: '',
      cpf: '',
      rg: '',
      generoId: '',
      telResidencial: '',
      telCelular: '',
      email: '',
      redeSocial: '',
      data: '',
      checkEndereco: '',
      logradouro: '',
      numero: '',
      complemento: '',
      bairro: '',
      cep: '',
      cidade: '',
      estado: '',
    });

    this.enderecoForm = this.fb.group({
      logradouro: '',
      numero: '',
      complemento: '',
      bairro: '',
      cep: '',
      cidade: '',
      estado: '',
    });

    this.sub = this.routeAc.params.subscribe(
      params => {
        this.clienteId = params['id'];
        this.obterCliente(this.clienteId);
      }
    );

    this.clienteService.obterCliente(this.clienteId)
    .subscribe(
      cliente => {
        this.cliente = cliente;
        this.enderecoMap = this.sanitizer.bypassSecurityTrustResourceUrl("https://www.google.com/maps/embed/v1/place?q=" + this.EnderecoCompleto() + "&key=AIzaSyAP0WKpL7uTRHGKWyakgQXbW6FUhrrA5pE");
      });

    this.clienteService.obterGeneros()
      .subscribe(genero => this.generos = genero,
      error => this.errors = error);
  }

  public EnderecoCompleto(): string {
    if (this.cliente.checkEndereco) {
      return this.cliente.endereco.logradouro + ", " + this.cliente.endereco.numero + " - " + this.cliente.endereco.bairro + ", " + this.cliente.endereco.cidade + " - " + this.cliente.endereco.estado;
    }
  }

  ngAfterViewInit(): void {
    let controlBlurs: Observable<any>[] = this.formInputElements
      .map((formControl: ElementRef) => Observable.fromEvent(formControl.nativeElement, 'blur'));

    Observable.merge(...controlBlurs).subscribe(value => {
      this.displayMessage = this.genericValidator.processMessages(this.clienteForm);
    });
  }

  obterCliente(id: string) {
    this.clienteService.obterCliente(id)
      .subscribe(
      cliente => this.preencherFormCliente(cliente),
      response => {
        if (response.status == 404) {
          this.router.navigate(['NotFound']);
        }
      });
  }

  preencherFormCliente(cliente: Cliente): void {
    this.cliente = cliente;

    this.clienteForm.patchValue({
      nomeCompleto: this.cliente.nomeCompleto,
      cpf: this.cliente.cpf,
      rg: this.cliente.rg,
      generoId: this.cliente.generoId,
      data: DateUtils.setMyDatePickerDate(this.cliente.data),
      telResidencial: this.cliente.telResidencial,
      telCelular: cliente.telCelular,
      email: this.cliente.email,
      redeSocial: this.cliente.redeSocial,
      checkEndereco: this.cliente.checkEndereco,
      
    });

    if (this.cliente.endereco) {
      this.enderecoForm.patchValue({
        logradouro: this.cliente.endereco.logradouro,
        numero: this.cliente.endereco.numero,
        complemento: this.cliente.endereco.complemento,
        bairro: this.cliente.endereco.bairro,
        cep: this.cliente.endereco.cep,
        cidade: this.cliente.endereco.cidade,
        estado: this.cliente.endereco.estado
      });
    }
  }

  editarCliente() {
    if (this.clienteForm.dirty && this.clienteForm.valid) {
      let p = Object.assign({}, this.cliente, this.clienteForm.value);
      
      (this.enderecoForm.dirty && this.enderecoForm.valid)
      let e = Object.assign({}, this.endereco, this.enderecoForm.value);
      e.clienteId = this.clienteId;

      let user = this.clienteService.obterUsuario();
      p.MasterUserId = user.id;
      p.data = DateUtils.getMyDatePickerDate(p.data);
     
      this.clienteService.adicionarEndereco(e)
      .subscribe(
      result => { this.onEnderecoSaveComplete() },
      fail => { this.onErrorEndereco(fail) });

      this.clienteService.atualizarClientes(p)
        .subscribe(
        result => { this.onSaveComplete() },
        error => { this.onError(error) });
    }
  }

  atualizarEndereco() {
    if (this.enderecoForm.dirty && this.enderecoForm.valid) {
      let p = Object.assign({}, this.endereco, this.enderecoForm.value);
      p.clienteId = this.clienteId;

      if (this.cliente.endereco) {
        p.id = this.cliente.endereco.id;
        this.clienteService.atualizarEndereco(p)
          .subscribe(
          result => { this.onEnderecoSaveComplete() },
          fail => { this.onErrorEndereco(fail) });
      }
      
    }
  }

  onEnderecoSaveComplete(): void {
    this.hideModal();

    this.toastr.success('Endereco Atualizado', 'Oba :D', {
      timeOut: 3000
    });
    this.router.navigate(['/clientes/meus-clientes']); 
    this.obterCliente(this.clienteId);
  }

  onSaveComplete() {
    this.errors = [];
    this.clienteForm.reset();

    this.toastr.success('Inscrição realizada com sucesso', 'Obrigado!', {
      timeOut: 3000
    });
    this.router.navigate(['/clientes/meus-clientes']);
  }


  onError(fail) {
    this.toastr.error('Ocorreu um erro no processamento', 'Ops! :(');
    this.errors = fail.error.errors;
  }

  onErrorEndereco(fail) {
    this.toastr.error('Ocorreu um erro no processamento do Endereço', 'Ops! :(');
    this.errorsEndereco = fail.error.errors;
  }

  public showModal(): void {
    this.modalVisible = true;
  }

  public hideModal(): void {
    this.modalVisible = false;
  }
}
