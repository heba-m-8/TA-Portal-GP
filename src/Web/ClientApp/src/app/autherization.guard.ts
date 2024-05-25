import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import notify from 'devextreme/ui/notify';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthorizationGuard implements CanActivate {
  constructor(private router: Router) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

    const token = localStorage.getItem('token');
    const user = JSON.parse(localStorage.getItem('user') || '{}');

    if (!token || !user.role) {
      notify('You are not authorized.');
      localStorage.clear();
      this.router.navigate(['']);
      return false;
    }

    if (state.url.indexOf(user.role.toLowerCase()) >= 0) {
      return true;
    } else {
      notify(`This page is not accessible for ${user.role}.`);
      localStorage.clear();
      this.router.navigate(['**']);
      return false;
    }
  }
}
