import { HttpClient } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { Component, NgModule} from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';


@Component({
  selector: 'app-login',
  imports: [CommonModule, FormsModule, HttpClientModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  loginModel = {
    email: '',
    password: '',
  };
  errorMessage = '';

  constructor(private http: HttpClient, private router: Router, private authService: AuthService) {}

  onLogin() {
    this.http.post('http://localhost:5100/api/Auth/login', this.loginModel)
      .subscribe(
        (response: any) => {
          console.log("Login response:", response.token); // Debugging
          this.authService.setToken(response.token);
          this.router.navigate(['/']);
        },
        (error) => {
          console.error("Login error:", error);
          this.errorMessage = error.error?.Message || 'Login failed';
        }
      );
  }
}