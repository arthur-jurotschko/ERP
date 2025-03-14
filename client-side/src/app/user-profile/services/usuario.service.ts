import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from 'rxjs/Observable';


import { Usuario, ResetPassword, ForgotPassword, ConfirmacaoEmail } from "../models/usuario";
import { BaseServices } from "app/services/base.services";





@Injectable()
export class UsuarioService extends BaseServices {

  constructor(private http: HttpClient) { super() }

  obterUsuarios(): Observable<Usuario[]> {
    return this.http
      .get<Usuario[]>(this.UrlServiceV1 + "GetAll-Users", super.obterAuthHeader())
      .catch(super.serviceError);

  }

  registrarUsuario(usuario: Usuario): Observable<Usuario> {
    let response = this.http
      .post(this.UrlServiceV1 + "nova-conta", usuario, super.ObterHeaderJson())
      .map(super.extractData)
      .catch(super.serviceError)
    return response;
  }

  login(usuario: Usuario): Observable<Usuario> {
    let response = this.http
      .post(this.UrlServiceV1 + "conta", usuario, super.ObterHeaderJson())
      .map(super.extractData)
      .catch(super.serviceError)
    return response;
  }

  ResetPassword(resetpassword: ResetPassword): Observable<ResetPassword> {
    let response = this.http
      .post(this.UrlServiceV1 + "ResetPassword", resetpassword, super.ObterHeaderJson())
      .map(super.extractData)
      .catch(super.serviceError)
    return response;
  }

  ForgotPassword(forgotpassword: ForgotPassword): Observable<ForgotPassword> {
    let response = this.http
      .post(this.UrlServiceV1 + "ForgotPassword", forgotpassword, super.ObterHeaderJson())
      .map(super.extractData)
      .catch(super.serviceError)
    return response;
  }

  ConfirmacaoEmail(confirmacaoemail: ConfirmacaoEmail): Observable<ConfirmacaoEmail> {
    let response = this.http
      .post(this.UrlServiceV1 + "ConfirmEmail", confirmacaoemail, super.ObterHeaderJson())
      .map(super.extractData)
      .catch(super.serviceError)
    return response;
  }

}


