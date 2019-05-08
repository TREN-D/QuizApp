import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';

import { ToastrService } from 'ngx-toastr';

import { AuthService } from '../../shared/auth.service';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.scss']
})
export class SignInComponent implements OnInit {
  @ViewChild('form') form: NgForm;
  showPassword = false;

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
      this.authService.signIn(email, password).subscribe(
        () => {
          this.router.navigateByUrl('/home');
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
