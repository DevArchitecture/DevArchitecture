import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from "@angular/router";
import { Observable } from "rxjs";
import { AuthService } from "../components/admin/login/services/auth.service";
import { LocalStorageService } from "../services/local-storage.service";


@Injectable()
export class LoginGuard implements CanActivate {

    constructor(private router: Router, private authService: AuthService) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
  
        if (this.authService.loggedIn()){
            return true;
        }
        this.router.navigate(["login"]);
        return false;

    }


}