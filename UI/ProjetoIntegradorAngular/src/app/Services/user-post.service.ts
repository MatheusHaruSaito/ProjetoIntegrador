import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { CreateUserPost } from '../models/CreateUserPost';
import { ViewUserPost } from '../models/ViewUserPost';
import { UpdateUserPost } from '../models/UpdateUserPost';
import { CreateVoteRequest } from '../models/CreateVoteRequest';
import { UserPost } from '../models/UserPost';
import { CreateCommentVoteRequest } from '../models/CreateCommentVoteRequest';
import { CreateComment } from '../models/CreateComment';

@Injectable({
  providedIn: 'root'
})
export class UserPostService {
  apiUrl = environment.ApiUrlUserPost;

  constructor(private http: HttpClient) { }

  // Accepts either CreateUserPost (object) or FormData (with optional file)
  Post(userPost: CreateUserPost | FormData): Observable<any> {
    if (userPost instanceof FormData) {
      // send multipart/form-data (no explicit headers for boundary)
      return this.http.post(this.apiUrl, userPost);
    } else {
      // send JSON body (no file)
      return this.http.post(this.apiUrl, userPost);
    }
  }

  GetAll(): Observable<ViewUserPost[]> {
    return this.http.get<ViewUserPost[]>(this.apiUrl);
  }

  GetById(id: string): Observable<UserPost> {
    return this.http.get<UserPost>(`${this.apiUrl}/${id}`);
  }

  Update(updatedPost: UpdateUserPost): Observable<boolean> {
    return this.http.put<boolean>(this.apiUrl, updatedPost);
  }

  ActivedTrigger(id: string): Observable<boolean> {
    return this.http.put<boolean>(`${this.apiUrl}/${id}`, '');
  }

  Vote(request: CreateVoteRequest): Observable<boolean> {
    return this.http.post<boolean>(`${this.apiUrl}/Vote`, request);
  }
  Comment(request: CreateComment){
    return this.http.put(`${this.apiUrl}/Comment`,request)
  }
  CommentVote(request: CreateCommentVoteRequest): Observable<boolean> {
    return this.http.post<boolean>(`${this.apiUrl}/CommentVote`, request);
  }
}
