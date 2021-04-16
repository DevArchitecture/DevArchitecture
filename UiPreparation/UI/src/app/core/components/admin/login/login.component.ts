import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { LookUp } from 'app/core/models/lookUp';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LocalStorageService } from 'app/core/services/local-storage.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { environment } from 'environments/environment';
import { LoginUser } from './model/login-user';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  username: string = "";
  loginUser: LoginUser = new LoginUser();
  langugelookUp: LookUp[];


  constructor(private auth: AuthService,
    private storageService: LocalStorageService,
    private lookupService: LookUpService,
    public translateService: TranslateService,
    private httpClient: HttpClient,
    private router: Router,
    private alertifyService: AlertifyService) { }

  ngOnInit() {

    this.username = this.auth.userName;

    this.httpClient.get<LookUp[]>(environment.getApiUrl + "/languages/getlookupwithcode").subscribe(data => {
      this.langugelookUp = data;
    })

  }

  getUserName() {
    return this.username;
  }

  login() {
    let result = this.auth.login(this.loginUser);
    
     result.then((value)=>{
       if(value)
      this.router.navigateByUrl("/dashboard");
      else
        this.alertifyService.warning("Check UserName and Password");

     })


  }

  logOut() {
    this.storageService.removeToken();
    this.storageService.removeItem("lang");
  }

  changeLang(lang) {
    localStorage.setItem("lang", lang);
    this.translateService.use(lang);
  }

}
