import { HostListener } from "@angular/core";
import { Component } from "@angular/core";
import { Jsonp } from "@angular/http";
import { NavigationStart, Router } from "@angular/router";
import { TranslateService } from "@ngx-translate/core";
import { Subscription } from "rxjs/Rx";
import { AuthService } from "./core/components/admin/login/services/auth.service";

export let browserRefresh = false;

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.css"],
})
export class AppComponent  {

  subscription: Subscription;
  isRefresh:boolean;

  constructor(
    private translate: TranslateService,
    private authService: AuthService,
    private router: Router
  ) {
    translate.setDefaultLang("tr-TR");
    translate.use("tr-TR");
    if (!this.authService.loggedIn()) {
      this.authService.logOut();
      this.router.navigateByUrl("/login");
    }

    this.subscription = router.events.subscribe((event) => {
      if (event instanceof NavigationStart) {
        browserRefresh = !router.navigated;
      }
  });
  }


  isLoggedIn(): boolean {
    return this.authService.loggedIn();
  }

}
