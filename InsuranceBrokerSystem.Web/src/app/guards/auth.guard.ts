import { CanActivateFn } from '@angular/router';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth.service';

export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  
  if (authService.isAuthenticatedValue) {
    return true;
  }
  
  // Redirect to login page with return URL
  window.location.href = '/login';
  return false;
};
