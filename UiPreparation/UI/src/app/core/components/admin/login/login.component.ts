import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { LookUp } from 'app/core/models/LookUp';
import { LocalStorageService } from 'app/core/services/LocalStorage.service';
import { LookUpService } from 'app/core/services/LookUp.service';
import { LoginUser } from './Model/LoginUser';
import { AuthService } from './Services/Auth.service';

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
    public translateService:TranslateService) { }

  ngOnInit() {

    this.username=this.auth.userName;

    this.lookupService.getLanguageLookup().subscribe(data=>{
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
    console.log(lang);
    localStorage.setItem("lang",lang);
    this.translateService.use(lang);
  }

}
