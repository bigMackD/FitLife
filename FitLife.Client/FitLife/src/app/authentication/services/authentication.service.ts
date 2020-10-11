import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { config } from '../../config';
import { RegisterRequest } from '../model/register/register.request';
import { Observable } from 'rxjs';
import { RegisterResponse } from '../model/register/register.response';
import { LoginRequest } from '../model/login/login.request';
import { LoginResponse } from '../model/login/login.response';
import { UserProfileResponse } from '../model/userProfile/userProfileResponse';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  userName: string;
  constructor(private httpClient: HttpClient) { }

  public getToken(): string {
    return localStorage.getItem('token');
  }

  public register(request:RegisterRequest):Observable<RegisterResponse>{
    return this.httpClient.post<RegisterResponse>(config.baseUrl + '/Users/Register', request);
  }

  public login(request:LoginRequest):Observable<LoginResponse>{
    return this.httpClient.post<LoginResponse>(config.baseUrl + '/Users/Login', request);
  }

  public getUserProfile():Observable<UserProfileResponse>{
    return this.httpClient.get<UserProfileResponse>(config.baseUrl + '/UserProfile');
  }

  public logout():void{
    this.userName = null;
    localStorage.removeItem('token');
  }

  public handleGetUserProfile(): void {
    this.getUserProfile().subscribe(response => {
      if (response.success) {
        this.userName = response.fullName;
      }
    });
  }

  isInRole(allowedRoles: Array<string>): boolean {
    var isMatch = false;
    var payload = JSON.parse(window.atob(localStorage.getItem('token').split('.')[1]))
    var userRole = payload.role;
    allowedRoles.forEach(element => {
      if(userRole == element){
        isMatch = true;
        return false;
      }
    });
    return isMatch;
  }

}
