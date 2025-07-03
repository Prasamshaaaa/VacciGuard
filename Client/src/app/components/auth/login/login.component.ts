import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Response } from 'src/app/shared/response.model';
import { HTTPResponseStatus, MessageBoxStatus } from 'src/app/shared/shared.enums';
import { ApplicationUser } from 'src/app/models/application-user.model';
import { MessageBoxService } from 'src/app/services/message-box.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  // Login form group for username and password
  public LoginForm!: FormGroup;

  constructor(
    private _fb: FormBuilder,
    private _authService: AuthService,
    private _router: Router,
    private _messageBoxService: MessageBoxService
  ) {}

  // Initialize form controls on component init
  public ngOnInit(): void {
    this.LoginForm = this._fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  // Getter for template binding to username control
  public get Username() {
    return this.LoginForm.get('username')!;
  }

  // Getter for template binding to password control
  public get Password() {
    return this.LoginForm.get('password')!;
  }

  /**
   * Submits the login form.
   * Validates inputs and calls AuthService.Login.
   * Shows message box on success or failure.
   */
  public OnSubmit(): void {
    if (this.LoginForm.invalid) {
      this._messageBoxService.ShowMessage(
        MessageBoxStatus.Warning,
        ['Please fill in all required fields.']
      );
      return;
    }

    const loginDto = this.LoginForm.value;

    this._authService.Login(loginDto).subscribe({
      next: (res: Response<ApplicationUser>) => {
        if (res.Status === HTTPResponseStatus.OK && res.Results) {
          localStorage.setItem('auth_user', JSON.stringify(res.Results));
          this._messageBoxService.ShowMessage(
            MessageBoxStatus.Success,
            ['Login successful! Redirecting...']
          );
          this._router.navigate(['/dashboard']);
        } else {
          this._messageBoxService.ShowMessage(
            MessageBoxStatus.Error,
            [res.ErrorMessage || 'Login failed. Please try again.']
          );
        }
      },
      error: (err) => {
        this._messageBoxService.ShowMessage(
          MessageBoxStatus.Error,
          ['An unexpected error occurred.']
        );
        console.error(err);
      }
    });
  }
}
