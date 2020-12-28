import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  constructor(private translateService: TranslateService) {
    translateService.addLangs(['en', 'klingon'])
    translateService.setDefaultLang('tr-TR');
    translateService.use(localStorage.getItem('lang') || 'tr-TR');
  }
}
