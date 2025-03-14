import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { HttpInterceptor, HttpHandler, HttpEvent, HttpRequest, HttpErrorResponse } from "@angular/common/http";

import 'rxjs/add/operator/catch';
import { Observable } from "rxjs";

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

    constructor(private router: Router) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req).catch(err => {
            if (err instanceof HttpErrorResponse) {
                if (err.status === 401) {
                    localStorage.removeItem('amj.token');
                    localStorage.removeItem('amj.user');
                    this.router.navigate(['/login']);
                }
                if (err.status === 403) {
                    this.router.navigate(['/acesso-negado']);
                }
                if (err.status === 404) {
                    this.router.navigate(['/nao-encontrado']);
                }
            }

            return Observable.throw(err);
        });
    }
}