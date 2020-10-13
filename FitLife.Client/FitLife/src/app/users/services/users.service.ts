import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UsersResponse } from '../models/users.response';
import { config } from '../../config';
import { Observable } from 'rxjs';
import { UsersRequest } from '../models/users.request';

@Injectable({
    providedIn: 'root'
  })
  export class UsersService {
  
    constructor(private httpClient: HttpClient) { }
    public getUsers(request:UsersRequest):Observable<UsersResponse>{
        return this.httpClient.get<UsersResponse>(config.baseUrl + '/Users/', {
          params: new HttpParams()
          // .set('filter', filter)
          .set('sortDirection', request.sortDirection)
          .set('pageIndex', request.pageIndex.toString())
          .set('pageSize', request.pageSize.toString())
        });
      }
  }