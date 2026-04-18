import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { AppComponent } from './app/app.component';

if (localStorage.getItem('devarch.darkMode') === 'true') {
  document.documentElement.classList.add('app-dark');
}

bootstrapApplication(AppComponent, appConfig)
  .catch((err) => console.error(err));
