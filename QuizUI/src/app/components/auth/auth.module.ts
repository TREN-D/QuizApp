import { NgModule } from '@angular/core';

import { library } from '@fortawesome/fontawesome-svg-core';
import { faUserTie, faEye, faEyeSlash } from '@fortawesome/free-solid-svg-icons';

import { MatTabsModule } from '@angular/material/tabs';

import { SharedModule } from '../shared/shared.module';

import { SignInComponent } from './components/sign-in/sign-in.component';
import { SignUpComponent } from './components/sign-up/sign-up.component';
import { AuthComponent } from './auth.component';
import { PasswordComparerDirective } from './entities/password-comparer.directive';

@NgModule({
  declarations: [
    SignInComponent,
    SignUpComponent,
    AuthComponent,
    PasswordComparerDirective
  ],
  imports: [
    MatTabsModule,
    SharedModule
  ]
})
export class AuthModule {
  constructor() {
    library.add(faUserTie, faEye, faEyeSlash);
  }
}
