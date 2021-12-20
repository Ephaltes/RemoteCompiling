import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../service/authentication.service';

@Component({
  selector: 'app-login-site',
  templateUrl: './login-site.component.html',
  styleUrls: ['./login-site.component.scss'],
  providers: [AuthService]
})
export class LoginSiteComponent implements OnInit {
  loginForm: FormGroup;
  validData: boolean;
  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router) {
    this.validData = false;
    this.loginForm = fb.group({ username: ['', Validators.required,], password: ['', Validators.required,], isUserLecturer: [false] })
  }

  ngOnInit(): void {
  }
  login() {
    if (this.loginForm.invalid)
      return;
    const value = this.loginForm.value;
    if (value.username && value.password) {
      this.authService.login(value.username, value.password).subscribe(() => { this.router.navigateByUrl('/platform') })
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
