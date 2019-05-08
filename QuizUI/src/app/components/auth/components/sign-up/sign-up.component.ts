import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroupDirective, FormControl, NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ErrorStateMatcher } from '@angular/material/core';

import { ToastrService } from 'ngx-toastr';

import { AuthService } from 'src/app/components/auth/shared/auth.service';

class CustomStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html'
})
export class SignUpComponent implements OnInit {
  customStateMatcher = new CustomStateMatcher();
  @ViewChild('form') form: NgForm;

  constructor(private authService: AuthService,
              private notification: ToastrService,
              private router: Router) {
   }

  ngOnInit() {
  }

  onSubmit() {
    const email = this.form.form.get('email').value;
    const password = this.form.form.get('password').value;

    if (this.form.valid) {
      this.authService.signUp(email, password)
      .subscribe(
        () => {
          this.notification.success('Now use credentials to login');
          this.router.navigateByUrl('/auth/sign-in');
        },
        (error: string) => {
          this.notification.error(error);
        }
        );
    } else {
      this.notification.error('Form is invalid!');
    }
  }
}
