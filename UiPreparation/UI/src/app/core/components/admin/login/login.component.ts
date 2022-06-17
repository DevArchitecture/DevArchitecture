import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { LookUp } from 'app/core/models/lookUp';
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

  username:string="";
  loginUser:LoginUser=new LoginUser();
  langugelookUp:LookUp[];


  constructor(private auth:AuthService,
    private storageService:LocalStorageService,
    private lookupService:LookUpService,
    public translateService:TranslateService,
    private httpClient:HttpClient) { }

  ngOnInit() {

    this.username=this.auth.userName;
    this.httpClient.get<LookUp[]>(environment.getApiUrl +"/languages/codes").subscribe(data=>{
      this.langugelookUp=data;
    })
    
  }

  getUserName(){
    return this.username;
  }

  login(){
    this.auth.login(this.loginUser);
  }

  logOut(){
      this.storageService.removeToken();
      this.storageService.removeItem("lang");
  }

  changeLang(lang){  
    localStorage.setItem("lang",lang);
    this.translateService.use(lang);
  }

}
