import { Component, OnInit } from '@angular/core';
import { Cliente, Endereco } from '../models/cliente';
import { Subscription } from 'rxjs';
import { ClientesService } from '../services/clientes.services';
import { ActivatedRoute, Router } from '@angular/router';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-detalhes-clientes',
  templateUrl: './detalhes-clientes.component.html',
  styleUrls: ['./detalhes-clientes.component.scss']
})
export class DetalhesClientesComponent implements OnInit {
  public sub: Subscription;
  public clienteId: string = "";
  public cliente: Cliente;
  public endereco: Endereco;
  public enderecoMap: any;

  constructor(private clienteService: ClientesService,
    private routeAc: ActivatedRoute,
    private router: Router,
    private sanitizer: DomSanitizer) { }

  ngOnInit() {

    this.cliente = new Cliente();
    this.cliente.endereco = new Endereco();

    this.sub = this.routeAc.params.subscribe(
      params => {
        this.clienteId = params['id'];

      });

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
}

