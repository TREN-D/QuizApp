import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../auth/shared/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  constructor(private authService: AuthService) {
   }

  ngOnInit() {
  }

  onSignOut() {
    this.authService.signOut();
  }
}
