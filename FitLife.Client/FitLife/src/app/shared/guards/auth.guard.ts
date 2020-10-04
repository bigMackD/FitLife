import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthenticationService } from 'src/app/authentication/services/authentication.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
 
  constructor( private router: Router, private authenticationService: AuthenticationService) { }
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      if(localStorage.getItem('token')!=null){
        let roles = next.data['permittedRoles'] as Array<string>;
        if(roles){
          return this.authenticationService.isInRole(roles);
        }
        return true;
      }
    else
    this.router.navigate(['login']);
    return false;
  }
  
}
