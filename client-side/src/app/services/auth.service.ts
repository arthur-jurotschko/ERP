import { Injectable } from "@angular/core";
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from "@angular/router";


@Injectable()
export class AuthService implements CanActivate {

    constructor(private router: Router){}

    
    public token: string;
    public user;

    canActivate(routeAc: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {

      
        this.token = localStorage.getItem('amj.token');
        this.user = JSON.parse(localStorage.getItem('amj.user'));

        if (!this.token) {
            this.router.navigate(['/login']);
        }

        let claim: any = routeAc.data[0];
        if (claim !== undefined) {
            let claim = routeAc.data[0]['claim'];

            if (claim) {
                if (!this.user.claims) {
                    this.router.navigate(['/acesso-negado']);

                }
            }
            let userClaims = this.user.claims.some(x => x.type === claim.nome && x.value === claim.valor);
            if(!userClaims){
                this.router.navigate(['/acesso-negado']);
            }
        }
        return true;
    }
}
