import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AdminLayoutRoutes } from '../components/app/layouts/admin-layout/admin-layout.routing';
import { DashboardComponent } from '../components/app/dashboard/dashboard.component';
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
import { LanguageComponent } from '../components/app/language/language.component';
import { TranslateComponent } from '../components/app/translate/translate.component';
import { OperationClaimComponent } from '../components/app/operationclaim/operationClaim.component';
import { TranslateLoader, TranslateModule, TranslatePipe } from '@ngx-translate/core';
import { HttpClient } from '@angular/common/http';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { TranslationService } from 'app/core/services/Translation.service';
import { DataTablesModule } from 'angular-datatables';


// export function layoutHttpLoaderFactory(http: HttpClient) {
// debugger;
//   return new TranslateHttpLoader(http,'../../../../../../assets/i18n/','.json');
// }

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
      loader:{
        provide:TranslateLoader,
        //useFactory:layoutHttpLoaderFactory,
        useClass:TranslationService,
        deps:[HttpClient]
      },
      isolate:true
    }),
    DataTablesModule 
  ],
  declarations: [
    DashboardComponent,
    UserComponent,
    LoginComponent,
    GroupComponent,
    LanguageComponent,
    TranslateComponent,
    OperationClaimComponent
    
  ]
})

export class AdminLayoutModule {}
