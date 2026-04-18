import { ApplicationConfig, provideZoneChangeDetection } from "@angular/core";
import { provideRouter } from "@angular/router";
import { provideHttpClient, withInterceptors } from "@angular/common/http";
import { providePrimeNG } from "primeng/config";
import Aura from "@primeuix/themes/aura";
import { provideAnimationsAsync } from "@angular/platform-browser/animations/async";
import { MessageService, ConfirmationService } from "primeng/api";

import { routes } from "./app.routes";
import { authInterceptor } from "./core/auth.interceptor";

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideAnimationsAsync(),
    provideHttpClient(withInterceptors([authInterceptor])),
    MessageService,
    ConfirmationService,
    providePrimeNG({
      theme: {
        preset: Aura,
        options: {
          darkModeSelector: ".app-dark"
        }
      }
    })
  ]
};
