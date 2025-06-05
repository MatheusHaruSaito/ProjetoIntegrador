import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { LoginUser } from '../models/LoginUser';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly JWT_Token= "JWT_TOKEN"
  private loggedUser?: string;
  private isAuthenticatedSubject = new BehaviorSubject<boolean>(false);
  private readonly ApiUrl = environment.ApiUrlAuth;
  constructor(private http: HttpClient) { }

  LogIn(user: LoginUser): Observable<any> {
    return this.http.post(this.ApiUrl + `/Login`, user).pipe(
      tap(res => this.doLoginUser(user.email,res))
    );
  }

  private doLoginUser(email: string, tokens:any){
    this.loggedUser = email;
    this.storeJwtToken(tokens.token);
    this.isAuthenticatedSubject.next(true);
  }
  private storeJwtToken(jwt:string){
    localStorage.setItem(this.JWT_Token,jwt);
  }

  Logout():void{
    localStorage.removeItem(this.JWT_Token);
    this.isAuthenticatedSubject.next(false);
  }
  GetUserFromToken(jwt: string | null): Observable<any> {
    return this.http.get<any>(`${this.ApiUrl}/${jwt}`);
  }
  isLoggedIn(){
    return !!localStorage.getItem(this.JWT_Token);
  }
}
