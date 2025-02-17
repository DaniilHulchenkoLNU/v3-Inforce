import { Component, OnInit } from '@angular/core';
import { AboutService } from '../../shared/services/about.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-edit-about',
  templateUrl: './edit-about.component.html',
  styleUrls: ['./edit-about.component.css']
})
export class EditAboutComponent implements OnInit {
  description: string = '';

  constructor(private aboutService: AboutService, private router: Router) { }

  ngOnInit(): void {
    this.loadDescription();
  }

  loadDescription(): void {
    this.aboutService.getDescription().subscribe({
      next: (data) => this.description = data.description,
      error: (err) => console.error('Error loading description', err)
    });
  }

  saveDescription(): void {
    this.aboutService.updateDescription(this.description).subscribe({
      next: () => this.router.navigate(['/about']),
      error: (err) => console.error('Error saving description', err)
    });
  }
}
