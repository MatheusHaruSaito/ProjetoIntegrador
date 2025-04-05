import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  ApiUrl = environment.ApiUrl;

  constructor(private http: HttpClient) { }

  GetUsers() : Observable<User[]>{
    return this.http.get<User[]>(this.ApiUrl)
  }
}
