import { Injectable } from '@angular/core';
import { HOST_URL, ACCESS_TOKEN, REFRESH_TOKEN } from 'src/app/config';
import { HttpClient } from '@angular/common/http';
import { IToken } from '../entities/IToken.interface';
import { of } from 'rxjs';
import { tap, flatMap } from 'rxjs/operators';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private httpClient: HttpClient, private router: Router) {
  }

  signIn(email: string, password: string) {

    const body = new URLSearchParams();
    body.set('username', email);
    body.set('password', password);
    body.set('client_id', 'AngularApp');
    body.set('grant_type', 'password');

    return this.httpClient.post(HOST_URL + 'token', body.toString()).pipe(
      tap(
        (data: IToken) => {
          this.setToken(data);
        }
      )
    );
  }

  signUp(email: string, password: string) {
    const body = { email, password };
    return this.httpClient.post(HOST_URL + 'api/auth/register', body);
  }

  getAccessToken(): string {
    return localStorage.getItem(ACCESS_TOKEN);
  }

  getRefreshToken(): string {
    return localStorage.getItem(REFRESH_TOKEN);
  }

  isAuthenticated(): boolean {
    const token = this.getAccessToken();
    return !!token;
  }

  signOut(): void {
    this.removeToken();
    this.router.navigateByUrl('/');
  }

  refresh() {
    const refreshToken = this.getRefreshToken();
    if (!!refreshToken) {
      this.removeToken();

      const body = new URLSearchParams();
      body.set('client_id', 'AngularApp');
      body.set('grant_type', 'refresh_token');
      body.set('refresh_token', refreshToken);

      return this.httpClient.post(HOST_URL + 'token', body.toString()).pipe(
        flatMap(
          (res: IToken) => {
            this.setToken(res);
            return of(true);
          }
        )
      );
    } else {
      this.signOut();
      return of(false);
    }
  }

  private removeToken(): void {
    localStorage.removeItem(ACCESS_TOKEN);
    localStorage.removeItem(REFRESH_TOKEN);
  }

  private setToken(token: IToken): void {
    localStorage.setItem(ACCESS_TOKEN, token.access_token);
    localStorage.setItem(REFRESH_TOKEN, token.refresh_token);
  }
}
