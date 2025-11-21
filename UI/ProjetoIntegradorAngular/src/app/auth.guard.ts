import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from './Services/auth.service';

export const authGuard: CanActivateFn = (route, state) => {
  let authService = inject(AuthService);
  let routerService = inject(Router);

  const requiredRoles = route.data['roles'] as string[]
  if(!authService.isLoggedIn()){
    routerService.navigate(['/']);
    return false
  }

  const userRoles = authService.getUserRoles()
  const hasRoles = requiredRoles.some(r => userRoles?.includes(r))

  if(!hasRoles){
    routerService.navigate(['/'])
    return false
  }
  return true;
};
