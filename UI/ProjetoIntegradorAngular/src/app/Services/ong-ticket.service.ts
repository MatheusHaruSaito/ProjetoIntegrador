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
  GetOngTicketList():Observable<OngTicket[]>{
    return this.http.get<OngTicket[]>(this.ApiUrl);
  };
  AcceptTicket(id:string):Observable<OngTicket>{
    return this.http.put<OngTicket>(`${this.ApiUrl}/${id}/accept`,'')
  }
  DeclineTicket(id:string):Observable<OngTicket>{
    return this.http.put<OngTicket>(`${this.ApiUrl}/${id}/decline/`,'')
  }
}
