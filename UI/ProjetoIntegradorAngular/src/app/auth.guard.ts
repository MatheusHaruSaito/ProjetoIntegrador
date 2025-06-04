import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from './Services/auth.service';

export const authGuard: CanActivateFn = (route, state) => {
  let authService = inject(AuthService);
  let routerService = inject(Router);
  if(!authService.isLoggedIn()){
    routerService.navigate(['/']);
    return false
  }
  return true;
};
