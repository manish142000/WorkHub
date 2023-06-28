import { Injectable, SkipSelf } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { User } from '../models/user.model';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpParams
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { exhaustMap, isEmpty, map, take, tap } from 'rxjs/operators';

@Injectable({providedIn: 'root'})
export class AuthInterceptorInterceptor implements HttpInterceptor  {

  constructor(
    private auth : AuthService
  ) {
    
  }

  intercept(request: HttpRequest<any>, next: HttpHandler) : Observable<HttpEvent<any>> {

    return this.auth.user.pipe(
      take(1), 
      exhaustMap( (user) => {
        if( user == null || ( user.token_expiration > new Date()) ){
          return next.handle(request);
        }
        const modifiedReq = request.clone({
          setHeaders: {
            Authorization: `Bearer ${user.jwt_token}`
          }
      });
        return next.handle(modifiedReq);        
      })
     )
  }
}
