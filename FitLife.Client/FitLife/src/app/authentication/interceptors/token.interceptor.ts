import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthenticationService } from '../services/authentication.service';
import { tap } from 'rxjs/operators';
import { Router } from '@angular/router';
import { NotificationService } from 'src/app/shared/services/notification.service';


@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  errorMsg = 'Something went wrong, please contact administrator!';
  constructor(private auth: AuthenticationService,
     private router:Router, private notificationService:NotificationService) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token = localStorage.getItem('token');
    if(token){
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${this.auth.getToken()}`
        }
      });
      return next.handle(request).pipe(
        tap(
          succ => {},
          err =>{
            if(err.status = 401){
              this.router.navigateByUrl('/login');
              this.auth.logout();
              this.notificationService.error(['You have been logged out due to inactivity!']) 
            }
            else{
              this.notificationService.error([this.errorMsg]) 
            }
          }
        )
      );
    }
    else{
      return next.handle(request.clone()).pipe(
        tap(
          succ => {},
          err =>{  
              this.notificationService.error([this.errorMsg]) 
          }
        )
      );
    } 
  }
}