import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { Injectable, } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, flatMap } from 'rxjs/operators';

import { AuthService } from './auth.service';
import { handleRequestError } from '../../shared/helpers/error-handler-helper';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private authService: AuthService) {
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const authReq = this.addAuthHeader(req);

    return next.handle(authReq).pipe(
      catchError(
        (error: any) => {
          if (error instanceof HttpErrorResponse) {
            switch (error.status) {
              case 401: {
                return this.handleUnauthorizedError(next, req);
              }
              case 0:
              case 500: {
                return this.handleInternalServerError();
              }
              default: {
                return throwError(error);
              }
            }
          } else {
            return throwError(error);
          }
        }),
      catchError(
        error => {
          return throwError(handleRequestError(error));
        }
      )
    );
  }

  private addAuthHeader(req: HttpRequest<any>): HttpRequest<any> {
    if (this.authService.isAuthenticated()) {
      const accessToken = this.authService.getAccessToken();
      req = req.clone({
        setHeaders: { Authorization: 'Bearer ' + accessToken }
      });
    }
    return req;
  }

  private handleUnauthorizedError(next: HttpHandler, req: HttpRequest<any>) {
    return this.authService.refresh().pipe(
      flatMap(
        (res) => {
          if (res) {
            return next.handle(this.addAuthHeader(req)).pipe
              (
                catchError((error: HttpErrorResponse) => {
                  if (error.status === 500) {
                    this.handleInternalServerError();
                  } else {
                    return throwError(error);
                  }
                })
              );
          } else {
            return throwError('Token not found, relogin please');
          }
        }
      )
    );
  }

  private handleInternalServerError() {
    this.authService.signOut();
    return throwError('Error occurred in the server part, relogin please');
  }
}
