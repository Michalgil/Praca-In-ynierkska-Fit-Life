import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Constants } from '../constans/Constans';

@Injectable(
)
export class AuthGuard implements CanActivate {
  constructor(private router: Router) { }

  canActivate() {
    if (localStorage.getItem(Constants.AUTH_TOKEN)) {
      return true;
    }

    this.router.navigate(['/login']);
    return false;
  }
}
