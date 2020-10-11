import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from './authentication/services/authentication.service';
import { NotificationService } from './shared/services/notification.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.sass']
})
export class AppComponent {
  title = 'FitLife';


  constructor(private notificationService: NotificationService,
    private authService: AuthenticationService,
    private router: Router) {
  }

  onLogout():void{
   this.authService.logout();
    this.notificationService.success('Successfully logged out!');
    this.router.navigate(['login'])
  }

  isLoggedIn():boolean{
    if(localStorage.getItem('token') != null)
      return true;
    else
    return false;  
  }
}
