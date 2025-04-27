import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../models/user';
import { PostUserDto } from '../models/PostUserDto';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  ApiUrl = environment.ApiUrlUser;

  constructor(private http: HttpClient) { }

  GetUsers() : Observable<User[]>{
    return this.http.get<User[]>(this.ApiUrl)
  }
  TriggerUserActive(id:string): Observable<boolean>{
    return this.http.put<boolean>(`${this.ApiUrl}/TriggerUserActive/${id}`,'');
  }
  PostUser(PostUser:PostUserDto) :Observable <User>{
    return this.http.post<User>(this.ApiUrl,PostUser);
  }
}
