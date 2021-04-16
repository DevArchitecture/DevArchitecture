import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LocalStorageService } from 'app/core/services/local-storage.service';
import { environment } from 'environments/environment';
import { LoginUser } from '../model/login-user';
import { TokenModel } from '../model/token-model';
import { SharedService } from 'app/core/services/shared.service';


@Injectable({
  providedIn: 'root'
})

export class AuthService {

 userName: string;
  isLoggin: boolean;
  decodedToken: any;
  userToken: string;
  jwtHelper: JwtHelperService = new JwtHelperService();
  claims: string[];

  constructor(private httpClient: HttpClient, private storageService: LocalStorageService, private sharedService:SharedService) {

    this.setClaims();
  }

  async login(loginUser: LoginUser): Promise<boolean> {

    let headers = new HttpHeaders();
    headers = headers.append("Content-Type", "application/json")

    var data= await this.httpClient.post<TokenModel>(environment.getApiUrl + "/Auth/login", loginUser, { headers: headers }).toPromise();
    
    if (data.success) {

      this.storageService.setToken(data.data.token);
      this.claims=data.data.claims;
      console.log(data.data.claims.length+",claims")
      var decode = this.jwtHelper.decodeToken(this.storageService.getToken());
      var propUserName = Object.keys(decode).filter(x => x.endsWith("/name"))[0];
      this.userName = decode[propUserName];
      this.sharedService.sendChangeUserNameEvent();
      return true;
      
    }
    else {
      return false;
    }


  }

  getUserName(): string {
    return this.userName;
  }

  setClaims() {

    if ((this.claims == undefined || this.claims.length == 0) && this.storageService.getToken() != null && this.loggedIn() ) {

      this.httpClient.get<string[]>(environment.getApiUrl + "/OperationClaims/getuserclaimsfromcache").subscribe(data => {
        this.claims =data;
      })

    
      var token = this.storageService.getToken();
      var decode = this.jwtHelper.decodeToken(token);

      var propUserName = Object.keys(decode).filter(x => x.endsWith("/name"))[0];
      this.userName = decode[propUserName];
    }
  }

  logOut() {
    this.storageService.removeToken();
    this.storageService.removeItem("lang")
    this.claims = [];
  }

  loggedIn(): boolean {

    let isExpired = this.jwtHelper.isTokenExpired(this.storageService.getToken());
    return !isExpired;
  }

  getCurrentUserId() {
    this.jwtHelper.decodeToken(this.storageService.getToken()).userId;
  }

  claimGuard(claim: string): boolean {

    var check = this.claims.some(function (item) {
      return item == claim;
    })

    return check;
  }
  
}
