import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PostUserDto } from '../models/PostUserDto';
import { User } from '../models/user';
import { UserProfile } from '../models/UserProfile';
import { UpdateUser } from '../models/UpdateUser';
import { UserProfileEditRequest } from '../models/UserProfileEditRequest';
import { ChangeUserPasswordRequest } from '../models/ChangeUserPasswordRequest';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  ApiUrl = environment.ApiUrlUser;

  constructor(private http: HttpClient) { }

  GetUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.ApiUrl)
  }
  GetUsersByEmail(email: string): Observable<User> {
    return this.http.get<User>(`${this.ApiUrl}/Email/${email}`)
  }
  UpdateUser(updateUser: UpdateUser): Observable<boolean> {
    const formData = new FormData();

    formData.append('id', updateUser.id)
    formData.append('name', updateUser.name);
    formData.append('email', updateUser.email);
    formData.append('password', updateUser.password);
    formData.append('description', updateUser.description);

    if (updateUser.profileImg) {
      formData.append('profileImg', updateUser.profileImg); // deve ser um File
    }
    return this.http.put<boolean>(this.ApiUrl, formData);
  }
  TriggerUserActive(id: string): Observable<boolean> {
    return this.http.put<boolean>(`${this.ApiUrl}/${id}`, '');
  }
  PostUser(PostUser: PostUserDto): Observable<User> {
    return this.http.post<User>(this.ApiUrl, PostUser);
  }
  GetUserById(id: string): Observable<User> {
    return this.http.get<User>(`${this.ApiUrl}/${id}`)
  }
  GetProfileInfo(id: String): Observable<UserProfile> {
    return this.http.get<UserProfile>(`${this.ApiUrl}/Profile/${id}`)
  }
  EditProfile(request: UserProfileEditRequest): Observable<boolean> {
     const formData = new FormData();

  formData.append("Id", request.id.toString());
  formData.append("Name", request.name);
  formData.append("Description", request.description);
  formData.append("Email", request.email);

  if (request.profileImg) {
    formData.append("ProfileImg", request.profileImg);
  }
    return this.http.put<boolean>(`${this.ApiUrl}/EditProfile`, formData)
  }
  ChangePassword(request: ChangeUserPasswordRequest): Observable<boolean> {
    return this.http.put<boolean>(`${this.ApiUrl}/ChangePassword`, request)
  }

}
