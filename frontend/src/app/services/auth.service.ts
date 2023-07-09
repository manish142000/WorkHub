import { Injectable, OnInit } from '@angular/core';
import { APIResponse } from '../Responses/apiresponse';
import { HttpClient } from '@angular/common/http';
import { SignUpData } from '../interfaces/sign-up-data';
import { LoginData } from '../interfaces/login-data';
import { BehaviorSubject, Observable, Subject, tap } from 'rxjs';
import { User } from '../models/user.model';
import { delay, map, take } from 'rxjs/operators';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
 
  user = new BehaviorSubject<User|null>(null); 

  expirationTimeout : any;

  //public user: Observable<User> = new Observable<User>();
  public isAuthenticated$ : Observable<boolean>;
  public isAuthenticatedDelay$ : Observable<boolean>;

  constructor(
    private http : HttpClient,
    private cookie : CookieService,
    private route : Router
  ) { 
    this.isAuthenticated$ = this.user.pipe(
      map( (user) => {
        if( !!user ){
          if( user.token_expiration < new Date() ){
            return false;
          }
          else{
            return true;
          }
        }
        return false;
      } ),
    );

    this.isAuthenticatedDelay$ = this.isAuthenticated$.pipe(
      delay(1500)
    )
  } 

  autoLogin(){
    const user_data : User = JSON.parse(this.cookie.get('userData'));


    if( !user_data || user_data.token_expiration > new Date() ){
      return;
    }

    this.user.next(user_data);
  }

  onLogout(){
    this.user.next(null);
    this.cookie.delete('userData');

    if( this.expirationTimeout ){
      clearTimeout(this.expirationTimeout);
    }

    this.route.navigate(["/"]);
  }

  autoLogout(expirationDuration : number){
    this.expirationTimeout = setTimeout( () => {
      this.onLogout();
    }, expirationDuration)
  }

  signUp(
   formData : {}
  ){
    return this.http.post<APIResponse>('https://localhost:7032/api/employee', formData).pipe(
      tap( (resData) => {
        const _user = new User(resData.result.toString(), resData.jwtToken, resData.expirationDate);
        this.user.next(_user);
        this.cookie.set('userData', JSON.stringify(_user));
        const duration = new Date(resData.expirationDate).getTime() - new Date().getTime();
        this.autoLogout(duration);
      })
    );
  }

  logIn( 
    data : LoginData
   ){
    return this.http.post<APIResponse>('https://localhost:7032/api/employee/login', data).pipe(
      tap( (resData) => {
        //console.log(resData);
        const _user = new User(resData.result.toString(), resData.jwtToken, resData.expirationDate);
        this.user.next(_user);
        this.cookie.set('userData', JSON.stringify(_user));
        const duration = new Date(resData.expirationDate).getTime() - new Date().getTime();
        this.autoLogout(duration);
        console.log(duration);
      })      
    );
   }
}
