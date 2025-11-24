import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { CreateEmail } from '../models/CreateEmail';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class EmailService {
  apiurl = environment.ApiUrlEmail

  constructor(private http : HttpClient) { }

  SendEmail(createEmail: CreateEmail){
    return this.http.post(this.apiurl,createEmail)
  }
}
