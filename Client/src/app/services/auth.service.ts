import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';
import { Response } from '../shared/response.model';
import { ApplicationUser } from '../models/application-user.model';
import { LoginDto } from '../dtos/login.dto';
import { RegisterDto } from '../dtos/register.dto';
import { ChangePasswordDto } from '../dtos/change-password.dto';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseUrl = `${environment.apiBaseUrl}/auth`;

  constructor(private http: HttpClient) {}

  Login(LoginDto: LoginDto): Observable<Response<ApplicationUser>> {
    return this.http.post<Response<ApplicationUser>>(`${this.baseUrl}/login`, LoginDto);
  }

  Register(RegisterDto: RegisterDto): Observable<Response<ApplicationUser>> {
    return this.http.post<Response<ApplicationUser>>(`${this.baseUrl}/register`, RegisterDto);
  }

  ChangePassword(ChangePasswordDto: ChangePasswordDto): Observable<Response<boolean>> {
    return this.http.post<Response<boolean>>(`${this.baseUrl}/change-password`, ChangePasswordDto);
  }

  IsLoggedIn(): boolean {
  return localStorage.getItem('auth_user') !== null;
}

}
