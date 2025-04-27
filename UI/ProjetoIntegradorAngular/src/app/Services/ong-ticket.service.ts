import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { OngTicket } from '../models/OngTicket';
import { PostOngTicket } from '../models/PostOngTicket';

@Injectable({
  providedIn: 'root'
})
export class OngTicketService {
  ApiUrl = environment.ApiUrlOngTicket;
  constructor(private http : HttpClient) { }

  PostOngTicket(postOngTicket : PostOngTicket): Observable<OngTicket>{
    return this.http.post<OngTicket>(this.ApiUrl,postOngTicket)
  };
}
