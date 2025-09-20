import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { CreateUserPost } from '../models/CreateUserPost';
import { ViewUserPost } from '../models/ViewUserPost';
import { UpdateUserPost } from '../models/UpdateUserPost';

@Injectable({
  providedIn: 'root'
})
export class UserPostService {
  apiUrl = environment.ApiUrlUserPost
  constructor(private http : HttpClient) { }
  
  Post(userPost : CreateUserPost): Observable<CreateUserPost>{
  return this.http.post<CreateUserPost>(this.apiUrl,userPost);
  }
  GetAll(): Observable<ViewUserPost[]>{
    return this.http.get<ViewUserPost[]>(this.apiUrl)
  }
  GetById(id:string): Observable<ViewUserPost>{
    return this.http.get<ViewUserPost>(`${this.apiUrl}/${id}`)
  }
  Update(updatedPost: UpdateUserPost ): Observable<boolean>{
    return this.http.put<boolean>(this.apiUrl,updatedPost)
  }
  ActivedTrigger(id: string): Observable<boolean>{
    return this.http.put<boolean>(`${this.apiUrl}/${id}`,'')
  }
}
