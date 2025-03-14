import { Component, OnInit, ViewChildren, ElementRef } from '@angular/core';
import { FormControlName } from '@angular/forms';
import { Subscription } from 'rxjs';
import { Cliente, Endereco } from '../models/cliente';
import { ClientesService } from '../services/clientes.services';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-excluir-clientes',
  templateUrl: './excluir-clientes.component.html',
  styleUrls: ['./excluir-clientes.component.scss']
})
export class ExcluirClientesComponent implements OnInit {
  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

  public sub: Subscription;
  public clienteId: string = "";
  public cliente: Cliente;
  public errors: any[] = [];
  public endereco: Endereco;
  public enderecoMap: any;

  constructor(private clienteService: ClientesService,
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService,
    private sanitizer: DomSanitizer) {

    this.cliente = new Cliente();
  }

  ngOnInit() {
    this.sub = this.route.params.subscribe(
      params => {
        this.clienteId = params['id'];
      });

    this.clienteService.obterCliente(this.clienteId)
      .subscribe(
      cliente => { this.cliente = cliente; },
      );

      this.clienteService.obterCliente(this.clienteId)
      .subscribe(
        cliente => {
          this.cliente = cliente;
          this.enderecoMap = this.sanitizer.bypassSecurityTrustResourceUrl("https://www.google.com/maps/embed/v1/place?q=" + this.EnderecoCompleto() + "&key=AIzaSyAP0WKpL7uTRHGKWyakgQXbW6FUhrrA5pE");
        });
  }

  public EnderecoCompleto(): string {
    if (this.cliente.checkEndereco) {
      return this.cliente.endereco.logradouro + ", " + this.cliente.endereco.numero + " - " + this.cliente.endereco.bairro + ", " + this.cliente.endereco.cidade + " - " + this.cliente.endereco.estado;
    }
  }

  public excluirCliente() {
    this.clienteService.excluirClientes(this.clienteId)
      .subscribe(
      cliente => { this.onDeleteComplete(cliente) },
      error => { this.onError() }
      );
  }

  public onDeleteComplete(ligacoes: any) {
    this.errors = [];
    this.toastr.success('Cliente Excluido com Sucesso!', 'Oba :D',{timeOut:3000})
    setTimeout(() => {
      this.router.navigate(['/clientes/meus-clientes']);
    }, 3000);
  }

  public onError() {
    this.toastr.error('Houve um erro no processamento!', 'Ops! :(');
  }
}
