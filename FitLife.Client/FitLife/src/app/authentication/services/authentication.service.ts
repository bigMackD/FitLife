import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RegisterRequest } from '../model/register.request';
import { Observable } from 'rxjs';
import { RegisterResponse } from '../model/register.response';
import { config } from '../../config';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor(private httpClient: HttpClient) { }

  public register(request:RegisterRequest):Observable<RegisterResponse>{
    return this.httpClient.post<RegisterResponse>(config.baseUrl + '/Users/Register', request);
  }
}
