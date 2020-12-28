import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClient, HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppRoutingModule } from './app.routing';
import { AppComponent } from './app.component';
import { AgmCoreModule } from '@agm/core';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { JwtModule } from '@auth0/angular-jwt';
import { ComponentsModule } from './core/modules/components.module';
import { AdminLayoutComponent } from './core/components/app/layouts/admin-layout/admin-layout.component';
import { CategoryComponent } from './modules/category/category.component';
import { AlertifyService } from './core/services/Alertify.service';
import { AuthService } from './core/components/admin/login/Services/Auth.service';
import { LocalStorageService } from './core/services/LocalStorage.service';
import { LoginGuard } from './core/guards/login-guard';
import { AuthInterceptorService } from './core/interceptors/AuthInterceptor.service';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { environment } from 'environments/environment';
import { TranslationService } from './core/services/Translation.service';

//  export function HttpLoaderFactory(http: HttpClient) {

//    return new TranslateHttpLoader(http,environment.getApiUrl+'/translates/gettranslatesbylang?lang=','');
//  }

 export function HttpLoaderFactory(http: HttpClient) {

  return new TranslateHttpLoader(http,'../assets/i18n/','.json');
}


export function tokenGetter() {
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
      config: {
        tokenGetter: tokenGetter
      }
    }),
    TranslateModule.forRoot({
       loader: {
        provide: TranslateLoader,
         //useClass: TranslationService,
         useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      },
      isolate:true
     })
  ],
  declarations: [
    AppComponent,
    AdminLayoutComponent
  ],
  providers: [AlertifyService, AuthService, LocalStorageService, LoginGuard, AuthInterceptorService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptorService,
      multi: true,
    },
    HttpClient

  ],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class AppModule { }
