import { Component, OnInit } from '@angular/core';
import { Cliente } from '../models/cliente';
import { SeoService, SeoModel } from 'app/services/seo.services';
import { ClientesService } from '../services/clientes.services';
import { OrderPipe } from 'ngx-order-pipe';

@Component({
  selector: 'app-meus-clientes',
  templateUrl: './meus-clientes.component.html',
  styleUrls: ['./meus-clientes.component.scss']
})
export class MeusClientesComponent implements OnInit {

  order: string = 'cliente.email';
  reverse: boolean = false;
  sortedCollection: any[];

  searchFilter: string;
  public cliente: Cliente[];
  errorMessage: string;
  p: number = 1;
  collection = [];


  constructor(seoService: SeoService, private clientesService: ClientesService, private orderPipe: OrderPipe) {


    let seoModel: SeoModel = <SeoModel>{
      title: 'Lista de Ligações',
      robots: 'Index, Follow',
      description: 'Lista das ligações',
      keywords: 'ligações, recados'
    };

    seoService.setSeoData(seoModel);

    this.sortedCollection = orderPipe.transform(this.collection, 'cliente.email');
    console.log(this.sortedCollection);
  }

  setOrder(value: string) {
    if (this.order === value) {
      this.reverse = !this.reverse;
    }

    this.order = value;
  }


  ngOnInit() {
    this.clientesService.obterMeusClientes()
      .subscribe(
        cliente => this.cliente = cliente,
        error => this.errorMessage =  error);
        
  }

}
