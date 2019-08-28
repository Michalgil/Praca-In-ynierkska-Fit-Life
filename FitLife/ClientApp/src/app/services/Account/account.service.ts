import { RegisterModel } from './../../Models/register-model';
import { SignIn } from './../../Models/sign-in';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError, mapTo, tap } from 'rxjs/operators';
import { of as observableOf } from 'rxjs/observable/of'
import { SignInResult } from '../../Models/sign-in-result';
import { Router } from '@angular/router';
import { Constants } from '../../common/constans';

@Injectable()
export class AccountService {
  constructor(
    private http: HttpClient,
    private router: Router) { }

  login(signIn: SignIn): Observable<SignInResult>{
    return this.http.post<SignInResult>("https://localhost:44348/api/Account/login",signIn);
  }

  register(registerModel: RegisterModel){
    return this.http.post("https://localhost:44348/api/Account/register",registerModel);
  }

  isloggedIn(){
    return !!localStorage.getItem(Constants.AUTH_TOKEN);
  }

  logoutUser(){
    localStorage.removeItem(Constants.AUTH_TOKEN);
    this.http.get("https://localhost:44348/api/Account/signout");
    this.router.navigate(['/login']);
    
  }
}
