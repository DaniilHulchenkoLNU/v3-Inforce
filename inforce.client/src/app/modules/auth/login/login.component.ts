import { Component } from '@angular/core';
import { AuthService } from '../../shared/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  username: string = '';
  password: string = '';
  errorMessage: string = '';

  ngOnInit() { }

  constructor(private authService: AuthService, private router: Router) { }

  login() {
    this.authService.login(this.username, this.password).subscribe({
      next: (res) => {
        //localStorage.setItem('token', res.token);
        this.router.navigate(['/urls']);
      },
      error: () => (this.errorMessage = 'Неверный логин или пароль.')
    });
  }
}
