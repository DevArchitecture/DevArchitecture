import { HostListener } from "@angular/core";
import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { TranslateService } from "@ngx-translate/core";
import { AuthService } from "./core/components/admin/login/services/auth.service";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.css"],
})
export class AppComponent {
  constructor(
    private translate: TranslateService,
    private authService: AuthService,
    private router: Router
  ) {
    debugger;
    translate.setDefaultLang("tr-TR");
    translate.use("tr-TR");
    if (!this.authService.loggedIn()) {
      this.authService.logOut();
      localStorage.clear();
      this.router.navigateByUrl("/login");
    }
  }

  isLoggedIn(): boolean {
    return this.authService.loggedIn();
  }
  @HostListener("window:beforeunload", ["$event"])
  public beforeunloadHandler($event) {
    debugger;
    if (this.authService.loggedIn() && $event.type=="beforeunload") {
      this.authService.logOut();
      localStorage.clear();
    }
  }

  @HostListener("window:keydown", ["$event"])
  public keydownHandler($event) {
    debugger;
    if (this.authService.loggedIn()) {
      
    }
  }
}
