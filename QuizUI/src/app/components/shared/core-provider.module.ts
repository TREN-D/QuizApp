import { NgModule } from '@angular/core';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { MAT_LABEL_GLOBAL_OPTIONS } from '@angular/material/core';

import { AuthInterceptor } from '../auth/shared/auth.interceptor';
import { LoadingInterceptor } from './shared/loading.interceptor';

import { AuthGuard } from '../auth/shared/auth.guard';

import { LoadingService } from './shared/loading.service';
import { AuthService } from '../auth/shared/auth.service';
import { UrlService } from './shared/url.service';
import { TestService } from './shared/test.service';
@NgModule({
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true},
    {provide: MAT_LABEL_GLOBAL_OPTIONS, useValue: {float: 'never'}},
    LoadingService,
    AuthService,
    UrlService,
    TestService,
    AuthGuard
  ]
})
export class CoreProviderModule {}
