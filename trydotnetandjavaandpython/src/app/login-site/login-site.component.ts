import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../service/authentication.service';
import { ActivatedRoute } from '@angular/router';
@Component({
  selector: 'app-login-site',
  templateUrl: './login-site.component.html',
  styleUrls: ['./login-site.component.scss'],
  providers: [AuthService]
})
export class LoginSiteComponent implements OnInit {
  loginForm: FormGroup;
  validData: boolean;
  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router, private route: ActivatedRoute) {
    this.validData = false;
    this.loginForm = fb.group({ username: ['', Validators.required,], password: ['', Validators.required,], isUserLecturer: [false] })
  }
  errorMessage: string;
  showError: boolean = false;
  ngOnInit(): void {
    if (this.authService.isLoggedIn()) {
      const isUserLecturer = localStorage.getItem("isuserlecturer");
      if (isUserLecturer == "1")
        this.router.navigateByUrl('platform');
      else
        this.router.navigateByUrl('coding');
    }
    this.route.queryParams
      .subscribe(params => {
        if (params.error == "loginExpired") {
          this.errorMessage = "Die Session ist abgelaufen! Bitte melden Sie sich neu an!";
          this.showError = true;
          this.authService.logout();
        }
      }
      );
  }
  login() {
    if (this.loginForm.invalid)
      return;
    const value = this.loginForm.value;
    if (value.username && value.password) {
      this.authService.login(value.username, value.password, value.isUserLecturer).subscribe(() => { this.router.navigateByUrl('/platform') })
    }
    this.validData = true;
    this.loginForm.reset();
  }
  get username() {
    return this.loginForm.get('username')!;
  }
  get password() {
    return this.loginForm.get('password')!;
  }
}
