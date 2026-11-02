import { AuthService } from '@/app/infrastructure/services/auth-service';
import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError, switchMap, throwError } from 'rxjs';

export const authInterceptor: HttpInterceptorFn = (req, next) => {

  const authService = inject(AuthService);
  const isAuthCall = req.url.toLowerCase().includes('/api/auth/');

  //Function that take a token as param and return either a clone of the request and add the Authorization with the token
  //or if there is no token, return the original request
  const withToken = (token: string | null) => token ? req.clone({setHeaders: {Authorization: `Bearer ${token}`}}) : req;

  return next(withToken(authService.AccessToken())).pipe(
    catchError(err => {
      if(err.status !== 401 || isAuthCall) return throwError(() => err);

      return authService.refreshToken().pipe(
        switchMap(res => next(withToken(res.accessToken))),
        catchError(refreshErr => {
          authService.clearSession();
          return throwError(() => refreshErr);
        })
      );
    })
  );
};
