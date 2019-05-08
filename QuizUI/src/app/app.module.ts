import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';

import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

import { CoreProviderModule } from './components/shared/core-provider.module';
import { SharedModule } from './components/shared/shared.module';
import { AppRoutingModule } from './app-routing.module';
import { AuthModule } from './components/auth/auth.module';

import { AppComponent } from './app.component';
import { NotFoundComponent } from './components/shared/components/not-found/not-found.component';
import { LoadingComponent } from './components/shared/components/loading/loading.component';

@NgModule({
  declarations: [
    AppComponent,
    NotFoundComponent,
    LoadingComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    MatProgressSpinnerModule,
    HttpClientModule,
    CoreProviderModule,
    AppRoutingModule,
    SharedModule,
    AuthModule
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
