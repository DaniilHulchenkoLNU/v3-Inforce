import { Component, OnInit } from '@angular/core';
import { AboutService } from '../../shared/services/about.service';
import { AuthService } from '../../shared/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.css']
})
export class AboutComponent implements OnInit {
  description: string = '';
  isAdmin: boolean = false;

  constructor(private aboutService: AboutService, public authService: AuthService, private router: Router) { }

  ngOnInit(): void {
    this.loadDescription();
    this.isAdmin = this.authService.isAdmin();
  }

  loadDescription(): void {
    this.aboutService.getDescription().subscribe({
      next: (data) => this.description = data.description,
      error: (err) => console.error('Error loading description', err)
    });
  }

  editDescription(): void {
    this.router.navigate(['/about/edit']);
  }
}
