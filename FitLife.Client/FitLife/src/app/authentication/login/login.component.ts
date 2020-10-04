import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AuthenticationService } from '../services/authentication.service';
import { Router } from '@angular/router';
import { LoginRequest } from '../model/login/login.request';
import { LoginResponse } from '../model/login/login.response';
import { NotificationService } from 'src/app/shared/services/notification.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.sass']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;

  constructor(private fb: FormBuilder,
    private authenticationService: AuthenticationService,
    private notificationService: NotificationService,
    private router: Router) { }

  ngOnInit() {
    this.constructForm();
  }

  private constructForm(): void {
    this.loginForm = this.fb.group({
      username: ['', [Validators.required]],
      password: ['', [Validators.required]],
    });
  }

  isFormValid(): boolean {
    return this.loginForm.valid;
  }

  private createRequest(): LoginRequest {
    return new LoginRequest(
      this.loginForm.get('username').value,
      this.loginForm.get('password').value,
    );
  }

  private handleResponse(response: LoginResponse): void {
    if (!response.success) {
      this.notificationService.error(response.errors);
    }
    else {
      localStorage.setItem('token',response.token);
      this.notificationService.success("Successfully logged in!");
      this.router.navigate(['/home']);
      this.authenticationService.getUserProfile().subscribe(response =>
        console.log(response))
    }
  }

  onSubmit() {
    const request = this.createRequest();
    this.authenticationService.login(request).subscribe(response =>
      this.handleResponse(response)
    );
  }
}
