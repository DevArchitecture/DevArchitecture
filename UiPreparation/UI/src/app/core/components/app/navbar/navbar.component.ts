import { Component, OnInit, ElementRef } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { AuthService } from '../../admin/login/services/auth.service';
import { SharedService } from 'app/core/services/shared.service';


@Component({
	selector: 'app-navbar',
	templateUrl: './navbar.component.html',
	styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

	userName: string;
	clickEventSubscription:Subscription;
	constructor(private authService: AuthService, private router: Router, private sharedService:SharedService) {

		this.clickEventSubscription= this.sharedService.getChangeUserNameClickEvent().subscribe(()=>{
			this.setUserName();
		  })

	}

	isLoggedIn(): boolean {

		return this.authService.loggedIn();
	}

	logOut() {
		this.authService.logOut();
		this.router.navigateByUrl("/login");

	}

	help(): void{

		window.open(
			'https://www.devarchitecture.net/',
			'_blank' 
		);
	}
	ngOnInit() {
		console.log(this.userName);
		this.userName = this.authService.getUserName();
	}

	setUserName(){

		this.userName = this.authService.getUserName();
	}
}
