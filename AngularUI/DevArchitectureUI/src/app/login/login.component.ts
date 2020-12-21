import { Component, OnInit } from '@angular/core';
import { LocalStorageService } from 'app/GlobalServices/LocalStorage.service';
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

  constructor(private auth:AuthService,
    private storageService:LocalStorageService) { }

  ngOnInit() {

    //debugger;
    this.username=this.auth.userName;
  }

  getUserName(){
    return this.username;
  }

  login(){
    this.auth.login(this.loginUser);
  }

  logOut(){
      this.storageService.removeToken();
  }

}
