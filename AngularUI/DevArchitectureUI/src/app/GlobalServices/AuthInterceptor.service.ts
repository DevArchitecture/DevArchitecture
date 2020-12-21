import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest,HttpResponse,HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from "rxjs";
import { retry } from 'rxjs-compat/operator/retry';
import { catchError } from 'rxjs/operators';
import { AlertifyService } from './Alertify.service';


@Injectable({
  providedIn: 'root'
})
export class AuthInterceptorService implements HttpInterceptor {
  constructor(private alertifyService:AlertifyService) {

  }
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    if (!req.url.endsWith("api/Auth/login")) {
      req = req.clone({
        setHeaders: {
          Authorization: `Bearer ${localStorage.getItem('token')}`
        },
        responseType:req.method=="DELETE"? "text":req.responseType
      });
      
    }

    return next.handle(req).pipe(
      catchError((error: HttpErrorResponse) => {
        let errorMessage = '';
        if (error.error instanceof ErrorEvent) {
          // client-side error
          errorMessage = `Hata: ${error.error.message}`;
        } else {
          // server-side error
          errorMessage = `Hata Kodu: ${error.status}\n Mesaj: ${error.error}`;
        }
        this.alertifyService.error(errorMessage);
        return throwError(errorMessage);
      })
    )
  }
}
