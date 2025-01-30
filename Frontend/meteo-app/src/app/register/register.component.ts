import { HttpClient } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { Component, NgModule} from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  imports: [CommonModule, FormsModule, HttpClientModule],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent {
  registerModel = {
    email: '',
    password: '',
  };
  errorMessage = '';

  constructor(private http: HttpClient, private router: Router) {}

  onRegister() {
    this.http
      .post('http://localhost:5100/Auth/register', this.registerModel)
      .subscribe(
        (response: any) => {
          alert(response.Message);
          this.router.navigate(['/login']);
        },
        (error) => {
          this.errorMessage = error.error?.errors?.[0]?.description || 'Registration failed';
        }
      );
  }
}
