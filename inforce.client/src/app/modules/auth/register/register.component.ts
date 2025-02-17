import { Component } from '@angular/core';
import { AuthService } from '../../shared/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  username: string = '';
  password: string = '';
  message: string = '';

  constructor(private authService: AuthService, private router: Router) { }

  register() {
    this.authService.register(this.username, this.password).subscribe({
      next: () => {
        this.message = 'Регистрация успешна! Теперь войдите.';
        setTimeout(() => this.router.navigate(['/auth/login']), 2000);
        this.message = 'регистрациия успешна !'
      },
      error: () => (this.message = 'Ошибка регистрации.')
    });
  }
}
