import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../admin/login/Services/Auth.service';


declare const $: any;
declare interface RouteInfo {
    path: string;
    title: string;
    icon: string;
    class: string;
    claim:string;
}
export const ROUTES: RouteInfo[] = [
    { path: '/user', title: 'Users',  icon: 'person', class: '', claim:"GetUsersQuery" },
    { path: '/group', title: 'Groups',  icon:'content_paste', class: '',claim:"GetGroupsQuery" },
    { path: '/product', title: 'Products',  icon:'library_books', class: '', claim:"GetProductsQuery" }
    // { path: '/icons', title: 'Customers',  icon:'bubble_chart', class: '' },
    // { path: '/maps', title: 'Relational Sample',  icon:'location_on', class: '' },
    // { path: '/notifications', title: 'Notifications',  icon:'notifications', class: '' }
];

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {
  menuItems: any[];

  constructor(private authService:AuthService) { 
  }

  ngOnInit() {
    this.menuItems = ROUTES.filter(menuItem => menuItem);
  }
  isMobileMenu() {
      if ($(window).width() > 991) {
          return false;
      }
      return true;
  };

  checkClaim(claim:string):boolean{
    return this.authService.claimGuard(claim)
  }
}
