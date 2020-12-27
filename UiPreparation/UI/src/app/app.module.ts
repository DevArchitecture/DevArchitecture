import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppRoutingModule } from './app.routing';
import { AppComponent } from './app.component';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { NgbModule} from '@ng-bootstrap/ng-bootstrap';
import { JwtModule } from '@auth0/angular-jwt';
import { ComponentsModule } from './core/modules/components.module';
import { AdminLayoutComponent } from './core/components/app/layouts/admin-layout/admin-layout.component';
import { CategoryComponent } from './modules/category/category.component';
import { AlertifyService } from './core/services/Alertify.service';
import { AuthService } from './core/components/admin/login/Services/Auth.service';
import { LocalStorageService } from './core/services/LocalStorage.service';
import { LoginGuard } from './core/guards/login-guard';
import { AuthInterceptorService } from './core/interceptors/AuthInterceptor.service';



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
