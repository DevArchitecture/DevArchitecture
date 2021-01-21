import { Component, OnInit, ElementRef } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../admin/login/services/auth.service';


@Component({
	selector: 'app-navbar',
	templateUrl: './navbar.component.html',
	styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

	userName: string;
	constructor(private authService: AuthService, private router: Router) {

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
			'https://calm-moss-01a78bc03.azurestaticapps.net',
			'_blank' 
		);
	}
	ngOnInit() {
		console.log(this.userName);
		this.userName = this.authService.getUserName();
	}
}
