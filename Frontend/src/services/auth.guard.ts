import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard {
  constructor(public router: Router) { }

  canActivate(): boolean {
    const jwt = sessionStorage.getItem('jwt');
    if (!jwt) {
      this.router.navigate(['login']);
      return false;
    }
    return true;
  }
}
