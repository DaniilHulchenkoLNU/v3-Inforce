import { Component, OnInit } from '@angular/core';
import { ShortenedUrl, UrlService } from '../../shared/services/url.service';
import { ShortUrl } from '../../../models/ShortUrl';

@Component({
  selector: 'app-url-table',
  templateUrl: './url-table.component.html',
  styleUrls: ['./url-table.component.scss']
})
export class UrlTableComponent implements OnInit {
  urls: ShortenedUrl[] = [];
  newUrl: string = '';
  errorMessage: string = '';

  constructor(private urlService: UrlService) { }

  ngOnInit() {
    this.loadUrls();
  }

  loadUrls() {
    this.urlService.getAllUrls().subscribe({
      next: (urls) => this.urls = urls,
      error: (err) => this.errorMessage = 'Err of loading links'
    });
  }

  shortenUrl() {
    this.urlService.shortenUrl(this.newUrl).subscribe({
      next: () => {
        this.newUrl = '';
        this.loadUrls();
      },
      error: () => this.errorMessage = 'Creating link error'
    });
  }

  deleteUrl(shortCode: string) {
    this.urlService.deleteUrl(shortCode).subscribe({
      next: () => this.loadUrls(),
      error: () => this.errorMessage = 'DEliting link error'
    });
  }
}
