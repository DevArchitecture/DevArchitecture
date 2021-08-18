import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpErrorResponse } from '@angular/common/http';
import { BehaviorSubject, Observable, throwError } from "rxjs";
import { catchError,filter,switchMap, take } from 'rxjs/operators';
import { TokenService } from '../components/admin/login/services/token.service';


@Injectable()
export class AuthInterceptorService implements HttpInterceptor {
  constructor(private tokenService:TokenService) {

  }
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    req = this.addToken(req);

    return next.handle(req).pipe(
      catchError((error) => {
        if (error instanceof HttpErrorResponse && error.status === 401) {
          return this.handle401Error(req,next);
        } 
        else {
          return throwError(error);
        }        
      })
    )
  }
  private addToken(req:HttpRequest<any>){
    var lang = localStorage.getItem("lang") || "tr-TR"
    
    //TODO: Refactoring needed
   
     return req.clone({
       setHeaders: {
         'Accept-Language': lang,
         'Authorization': `Bearer ${localStorage.getItem('token')}`

       },
      
       responseType: req.method == "DELETE" ? "text" : req.responseType
     });

   
  }
  private isRefreshing = false;
  private refreshTokenSubject: BehaviorSubject<any> = new BehaviorSubject<any>(null);

  private handle401Error(req: HttpRequest<any>, next: HttpHandler):Observable<HttpEvent<any>> {
    if(!this.isRefreshing)  {
      this.isRefreshing=true;
      this.refreshTokenSubject.next(null);

      return this.tokenService.refreshToken().pipe(
       switchMap((token:any) => {
         console.log("Token Yenilendi.")
         this.isRefreshing = false;
         this.refreshTokenSubject.next(token.data.token);
         return next.handle(this.addToken(req))
       }));
    }
    else{
        return this.refreshTokenSubject.pipe(
          filter(token => token != null),
          take(1),
          switchMap(jwt => {
            return next.handle(this.addToken(req));
          })
        )
    }
  }
  
}
