import { Routes } from '@angular/router';
import { GroupComponent } from 'app/core/components/admin/group/group.component';
import { LoginComponent } from 'app/core/components/admin/login/login.component';
import { UserComponent } from 'app/core/components/admin/user/user.component';
import { LoginGuard } from 'app/core/guards/login-guard';
import { ProductComponent } from 'app/modules/product/product.component';
import { DashboardComponent } from '../../dashboard/dashboard.component';



export const AdminLayoutRoutes: Routes = [

    { path: 'dashboard',      component: DashboardComponent,canActivate:[LoginGuard] }, 
    { path: 'user',           component: UserComponent, canActivate:[LoginGuard] },
    { path: 'group',          component: GroupComponent, canActivate:[LoginGuard] },
    { path: 'product',        component: ProductComponent, canActivate:[LoginGuard] },
    { path: 'login',          component:LoginComponent },
    
];
