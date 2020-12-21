import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';


import { AppRoutingModule } from './app.routing';
import { ComponentsModule } from './components/components.module';

import { AppComponent } from './app.component';

import { DashboardComponent } from './dashboard/dashboard.component';

import {
  AgmCoreModule
} from '@agm/core';
import { AdminLayoutComponent } from './layouts/admin-layout/admin-layout.component';
import { UserComponent } from './user/user.component';
import {AlertifyService} from '../app/GlobalServices/Alertify.service';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { NgbModule} from '@ng-bootstrap/ng-bootstrap';
import { JwtModule } from '@auth0/angular-jwt';
import { AuthService } from './login/Services/Auth.service';
import { LocalStorageService } from './GlobalServices/LocalStorage.service';
import { LoginGuard } from './login/login-guard';
import { AuthInterceptorService } from './GlobalServices/AuthInterceptor.service';
import { CategoryComponent } from './category/category.component';


export function tokenGetter() {
  debugger;
  return localStorage.getItem("token");
}

@NgModule({
  imports: [
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    ComponentsModule,
    RouterModule,
    AppRoutingModule,
    AgmCoreModule.forRoot({
      apiKey: 'YOUR_GOOGLE_MAPS_API_KEY'
    }),
    NgMultiSelectDropDownModule.forRoot(),
    NgbModule,
    JwtModule.forRoot({
      config:{
        tokenGetter:tokenGetter
      }
    })
  ],
  declarations: [			
    AppComponent,
    AdminLayoutComponent,
    CategoryComponent
   ],
  providers: [AlertifyService,AuthService,LocalStorageService,LoginGuard,AuthInterceptorService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptorService,
      multi: true,
    }  
  
  ],
  bootstrap: [AppComponent],
  schemas: [ CUSTOM_ELEMENTS_SCHEMA],
})
export class AppModule { }
