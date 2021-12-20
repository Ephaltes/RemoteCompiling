import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../service/authentication.service';

@Component({
  selector: 'app-logged-in-user',
  templateUrl: './logged-in-user.component.html',
  styleUrls: ['./logged-in-user.component.scss'],
  providers: [AuthService]
})
export class LoggedInUserComponent implements OnInit {

  constructor(private router: Router, private authService: AuthService) { }
  isLoggedin = false;
  ngOnInit(): void {
    if (this.authService.isLoggedIn()) this.isLoggedin = true
    else this.isLoggedin = false;
  }
  login() {
    this.router.navigateByUrl('/login');
  }
  logout() {
    this.authService.logout();
    this.router.navigateByUrl('/login');
  }

}
