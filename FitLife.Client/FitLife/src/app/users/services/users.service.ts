import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UsersResponse } from '../models/users.response';
import { config } from '../../config';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
  })
  export class UsersService {
  
    constructor(private httpClient: HttpClient) { }
    public getUsers():Observable<UsersResponse>{
        return this.httpClient.get<UsersResponse>(config.baseUrl + '/Users/');
      }
  }