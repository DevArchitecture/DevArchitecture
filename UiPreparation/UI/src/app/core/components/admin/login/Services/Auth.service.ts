import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LocalStorageService } from 'app/core/services/local-storage.service';
import { environment } from 'environments/environment';
import { ExternalLoginUser, LoginUser } from '../model/login-user';
import { TokenModel } from '../model/token-model';
import { SharedService } from 'app/core/services/shared.service';
import { SocialAuthService, GoogleLoginProvider, SocialUser, FacebookLoginProvider } from "angularx-social-login";


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

  constructor(private httpClient: HttpClient, private storageService: LocalStorageService, 
    private router: Router, private alertifyService:AlertifyService, private sharedService: SharedService,
    private externalAuthService: SocialAuthService,) {

    this.setClaims();
  }

  login(loginUser: LoginUser) {

    let headers = new HttpHeaders();
    headers = headers.append("Content-Type", "application/json")

    this.httpClient.post<TokenModel>(environment.getApiUrl + "/Auth/login", loginUser, { headers: headers }).subscribe(data => {
      this.setToken(data);
    });
  }

  googleLogin() {
    this.externalAuthService.signIn(GoogleLoginProvider.PROVIDER_ID)
      .then(res => {
        const user: SocialUser = { ...res };
        const externalAuth: ExternalLoginUser = {
          provider: user.provider,
          token: user.idToken
        };
        let headers = new HttpHeaders();
        headers = headers.append("Content-Type", "application/json")

        this.httpClient.post<TokenModel>(environment.getApiUrl + "/Auth/external-login", externalAuth, { headers: headers }).subscribe(data => {
          this.setToken(data);
        });
    });
  }

  facebookLogin() {
    this.externalAuthService.signIn(FacebookLoginProvider.PROVIDER_ID)
      .then(res => {
        const user: SocialUser = { ...res };
        const externalAuth: ExternalLoginUser = {
          provider: user.provider,
          token: user.authToken
        };
        let headers = new HttpHeaders();
        headers = headers.append("Content-Type", "application/json")

        this.httpClient.post<TokenModel>(environment.getApiUrl + "/Auth/external-login", externalAuth, { headers: headers }).subscribe(data => {
          this.setToken(data);
        });
    });
  }

  setToken(data: TokenModel) {
    if (data.success) {

      this.storageService.setToken(data.data.token);
      this.storageService.setItem("refreshToken",data.data.refreshToken)
      this.claims=data.data.claims;


       var decode = this.jwtHelper.decodeToken(this.storageService.getToken());


      var propUserName = Object.keys(decode).filter(x => x.endsWith("/name"))[0];
      this.userName = decode[propUserName];
      this.sharedService.sendChangeUserNameEvent();

      this.router.navigateByUrl("/dashboard");
    }
    else {
      this.alertifyService.warning(data.message);
    }
  }

  getUserName(): string {
    return this.userName;
  }

  setClaims() {

    if ((this.claims == undefined || this.claims.length == 0) && this.storageService.getToken() != null && this.loggedIn() ) {

      this.httpClient.get<string[]>(environment.getApiUrl + "/operation-claims/cache").subscribe(data => {
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
    this.storageService.removeItem("refreshToken");
    this.claims = [];
  }

  loggedIn(): boolean {

    let isExpired = this.jwtHelper.isTokenExpired(this.storageService.getToken(),-120);
    return !isExpired;
  }

  getCurrentUserId() {
    this.jwtHelper.decodeToken(this.storageService.getToken()).userId;
  }

  claimGuard(claim: string): boolean {
    if(!this.loggedIn())
     this.router.navigate(["/login"]);
    var check = this.claims.some(function (item) {
      return item == claim;
    })

    return check;
  }
  
}
