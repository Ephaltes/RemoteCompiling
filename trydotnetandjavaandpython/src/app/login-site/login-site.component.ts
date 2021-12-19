import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-login-site',
  templateUrl: './login-site.component.html',
  styleUrls: ['./login-site.component.scss']
})
export class LoginSiteComponent implements OnInit {
  loginForm: FormGroup;
  validData: boolean;
  constructor(private fb: FormBuilder) {
    this.validData = false;
    this.loginForm = fb.group({ username: ['', Validators.required,], password: ['', Validators.required,], isUserLecturer: false })
  }

  ngOnInit(): void {
  }
  submit() {
    if (this.loginForm.invalid)
      return;
    
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
