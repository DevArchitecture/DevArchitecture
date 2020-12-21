import { Component, OnInit, ElementRef } from '@angular/core';
import { ROUTES } from '../sidebar/sidebar.component';
import {Location, LocationStrategy, PathLocationStrategy} from '@angular/common';
import { Router } from '@angular/router';
import { AuthService } from 'app/login/Services/Auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

    userName:string="deneme";
    constructor(private authService:AuthService, private router:Router) {

    }

    isLoggedIn(): boolean {

        return this.authService.loggedIn();
    }

    logOut(){
        this.authService.logOut();
        this.router.navigateByUrl("/login");

    }

    ngOnInit(){
      
        console.log(this.userName);
        this.userName=this.authService.getUserName();
        
    }


}
