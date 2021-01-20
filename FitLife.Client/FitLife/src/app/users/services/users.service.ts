import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UsersResponse } from '../models/users.response';
import { config } from '../../config';
import { Observable } from 'rxjs';
import { UsersRequest } from '../models/users.request';
import { UserDetailsRequest } from '../models/details/userDetails.request';
import { UserDetailsResponse } from '../models/details/userDetails.response';
import { DisableUserRequest } from '../models/disable/disable.request';
import { DisableUserResponse } from '../models/disable/disable.response';
import { EnableUserRequest } from '../models/enable/enable.request';
import { EnableUserResponse } from '../models/enable/enable.response';

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

      public getDetails(request:UserDetailsRequest):Observable<UserDetailsResponse>{
        return this.httpClient.get<UserDetailsResponse>(config.baseUrl + '/Users/' + request.id);
      }

      public disable(request:DisableUserRequest):Observable<DisableUserResponse>{
        return this.httpClient.get<UserDetailsResponse>(config.baseUrl + '/Users/disable/' + request.id);
      }

      public enable(request:EnableUserRequest):Observable<EnableUserResponse>{
        return this.httpClient.get<EnableUserResponse>(config.baseUrl + '/Users/enable/' + request.id);
      }
  }