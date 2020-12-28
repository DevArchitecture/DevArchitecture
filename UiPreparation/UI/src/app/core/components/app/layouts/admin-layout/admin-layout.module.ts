import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AdminLayoutRoutes } from './admin-layout.routing';
import { DashboardComponent } from '../../dashboard/dashboard.component';
import {MatButtonModule} from '@angular/material/button';
import {MatInputModule} from '@angular/material/input';
import {MatRippleModule} from '@angular/material/core';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatTooltipModule} from '@angular/material/tooltip';
import {MatSelectModule} from '@angular/material/select';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { LoginComponent } from 'app/core/components/admin/login/login.component';
import { GroupComponent } from 'app/core/components/admin/group/group.component';
import { UserComponent } from 'app/core/components/admin/user/user.component';
import { ProductComponent } from 'app/modules/product/product.component';
import { LanguageComponent } from '../../language/language.component';
import { TranslateComponent } from '../../translate/translate.component';
import { OperationClaimComponent } from '../../operationclaim/operationClaim.component';
import { TranslateLoader, TranslateModule, TranslatePipe } from '@ngx-translate/core';
import { HttpClient } from '@angular/common/http';
import { TranslationService } from 'app/core/services/Translation.service';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';

export function HttpLoaderFactory(http: HttpClient) {

  return new TranslateHttpLoader(http,'../assets/i18n/','.json');
}

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(AdminLayoutRoutes),
    FormsModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatRippleModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatTooltipModule,
    NgbModule,
    NgMultiSelectDropDownModule,
    TranslateModule.forChild({
      loader: {
       provide: TranslateLoader,
        //useClass: TranslationService,
        useFactory: HttpLoaderFactory,
       deps: [HttpClient]
     },

    })


  ],
  declarations: [
    DashboardComponent,
    UserComponent,
    LoginComponent,
    GroupComponent,
    ProductComponent,
    LanguageComponent,
    TranslateComponent,
    OperationClaimComponent
    
  ]
})

export class AdminLayoutModule {}
