import { BaseServices } from "app/services/base.services";
import { HttpClient } from '@angular/common/http';
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Cliente, Genero, Endereco } from "../models/cliente";
import { DashboardSum } from "../models/dashboardsum";

@Injectable()

export class ClientesService extends BaseServices {

    constructor(private http: HttpClient) { super(); }

    obterTodos(): Observable<Cliente[]> {
        return this.http
            .get<Cliente[]>(this.UrlServiceV1 + "obterclientes", super.obterAuthHeader())
            .catch(super.serviceError);
    };

    obterGeneros(): Observable<Genero[]> {
        return this.http
            .get<Genero[]>(this.UrlServiceV1 + "genero")
            .catch(super.serviceError);
    };

    obterCliente(id: string): Observable<Cliente> {
        return this.http
            .get<Cliente>(this.UrlServiceV1 + "cliente-id/" + id)
            .catch(super.serviceError);
    };

    obterMeusClientes(): Observable<Cliente[]> {
        return this.http
            .get<Cliente[]>(this.UrlServiceV1 + "meus-clientes", super.obterAuthHeader())
            .catch(super.serviceError);
    };
  

    registrarClientes(cliente: Cliente): Observable<Cliente> {
        let response = this.http
            .post(this.UrlServiceV1 + "novo-cliente", cliente, super.obterAuthHeader())
            .map(super.extractData)
            .catch(super.serviceError)
        return response;
    };

    atualizarClientes(cliente: Cliente): Observable<Cliente> {
        let response = this.http
            .put(this.UrlServiceV1 + "atualizar-cliente", cliente, super.obterAuthHeader())
            .map(super.extractData)
            .catch((super.serviceError));
        return response;
    };

    excluirClientes(id: string): Observable<Cliente> {
        let response = this.http
            .delete(this.UrlServiceV1 + "excluir-cliente/" + id, super.obterAuthHeader())
            .map(super.extractData)
            .catch((super.serviceError));
        return response;
    };
    adicionarEndereco(endereco: Endereco): Observable<Endereco> {
        let response = this.http
            .post(this.UrlServiceV1 + "adicionar-endereco", endereco, super.obterAuthHeader())
            .map(super.extractData)
            .catch((super.serviceError));
        return response;
    };

    atualizarEndereco(endereco: Endereco): Observable<Endereco> {
        let response = this.http
            .put(this.UrlServiceV1 + "atualizar-endereco", endereco, super.obterAuthHeader())
            .map(super.extractData)
            .catch((super.serviceError));
        return response;
    };

    infoGraf(): Observable<DashboardSum[]>  {
        return this.http
        .get<DashboardSum[]>(this.UrlServiceV1 + "getdashboard")
        .catch(super.serviceError);
    };
   
    adicionarClienteDashboard(): Observable<DashboardSum[]>  {
        return this.http
        .get<DashboardSum[]>(this.UrlServiceV1 + "adicionarClienteDashboard")
        .catch(super.serviceError);
    };

}