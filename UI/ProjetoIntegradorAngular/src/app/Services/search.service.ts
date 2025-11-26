import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { Observable } from 'rxjs';
import { SearchResult } from '../models/SearchResult';
import { SearchTypeEnum } from '../models/SearchTypeEnum';

@Injectable({
  providedIn: 'root'
})
export class SearchService {
  ApiUrl = environment.ApiUrlSearch
  http = inject(HttpClient)
  constructor() { }

  Search(q:string, type:SearchTypeEnum = SearchTypeEnum.All ,page: number = 1,pageSize: number = 20): Observable<SearchResult>{

    let params = new HttpParams()
    .set('q',q)
    .set('type',type)
    .set('page',page)
    .set('pageSize',pageSize)
    
    return this.http.get<SearchResult>(this.ApiUrl, { params })
  }

}
