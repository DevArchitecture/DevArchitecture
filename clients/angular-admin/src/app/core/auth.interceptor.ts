import { HttpInterceptorFn } from "@angular/common/http";
import { inject } from "@angular/core";
import { Router } from "@angular/router";
import { catchError, throwError } from "rxjs";
import { AuthService } from "./auth.service";
import { environment } from "../../environments/environment";

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  const router = inject(Router);
  const token = authService.getToken();

  const withHeaders = req.clone({
    setHeaders: {
      "x-dev-arch-version": environment.apiVersion,
      ...(token ? { Authorization: `Bearer ${token}` } : {})
    }
  });

  return next(withHeaders).pipe(
    catchError((error) => {
      if (error?.status === 401) {
        authService.logout();
        if (!router.url.startsWith("/login")) {
          void router.navigateByUrl("/login");
        }
      }
      return throwError(() => error);
    })
  );
};
