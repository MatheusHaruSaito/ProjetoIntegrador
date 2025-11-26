import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { LoginUser } from '../models/LoginUser';
import { jwtDecode } from "jwt-decode";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private userSubject = new BehaviorSubject<any>(null);
  user$ = this.userSubject.asObservable();
  RefreshSession() {
    const updatedUser = this.GetUserFromJwtToken();
    localStorage.setItem("UserLogged", JSON.stringify(updatedUser));
  }
  private readonly JWT_Token = "JWT_TOKEN"
  private loggedUser?: string;
  private isAuthenticatedSubject = new BehaviorSubject<boolean>(false);
  private readonly ApiUrl = environment.ApiUrlAuth;
  constructor(private http: HttpClient) {
  }

  LogIn(user: LoginUser): Observable<any> {
    return this.http.post(this.ApiUrl, user).pipe(
      tap(res => this.doLoginUser(user.email, res))
    );
  }

  private doLoginUser(email: string, tokens: any) {
    this.loggedUser = email;
    this.storeJwtToken(tokens.token);
    this.isAuthenticatedSubject.next(true);
  }
  private storeJwtToken(jwt: string) {
    localStorage.setItem(this.JWT_Token, jwt);
  }
  private getJwtToken(): string | null {
    if (typeof window !== 'undefined' && window.localStorage) {
      return localStorage.getItem('JWT_TOKEN');
    }
    return null;
  }

  Logout(): void {
    localStorage.removeItem(this.JWT_Token);
    this.isAuthenticatedSubject.next(false);
  }
  GetUserFromJwtToken() {
    var token = this.getJwtToken()!
    var decodedToken: any = jwtDecode(token)
    const userDetail = {
      id: decodedToken.nameid,
      email: decodedToken.email,
      name: decodedToken.unique_name,
    }
    return userDetail;
  }
  isLoggedIn(): Observable<boolean> {
    this.isAuthenticatedSubject.next(!!this.getJwtToken())
    return this.isAuthenticatedSubject.asObservable()
  }
}
