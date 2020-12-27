import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from "@angular/router";
import { Observable } from "rxjs";
import { AuthService } from "../components/admin/login/Services/Auth.service";
import { LocalStorageService } from "../services/LocalStorage.service";


@Injectable()
export class LoginGuard implements CanActivate {

    constructor(private router: Router, storageService: LocalStorageService, private authService: AuthService) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
        console.log(this.authService.loggedIn());
        if (this.authService.loggedIn()){
            return true;
        }
        this.router.navigate(["login"]);
        return false;

    }


}