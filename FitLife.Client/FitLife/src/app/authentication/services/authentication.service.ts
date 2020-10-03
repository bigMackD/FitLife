import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { config } from '../../config';
import { RegisterRequest } from '../model/register/register.request';
import { Observable } from 'rxjs';
import { RegisterResponse } from '../model/register/register.response';
import { LoginRequest } from '../model/login/login.request';
import { LoginResponse } from '../model/login/login.response';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor(private httpClient: HttpClient) { }

  public register(request:RegisterRequest):Observable<RegisterResponse>{
    return this.httpClient.post<RegisterResponse>(config.baseUrl + '/Users/Register', request);
  }

  public login(request:LoginRequest):Observable<LoginResponse>{
    return this.httpClient.post<LoginResponse>(config.baseUrl + '/Users/Login', request);
  }

 
}
