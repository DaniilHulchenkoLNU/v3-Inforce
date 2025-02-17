import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UrlService, ShortenedUrl } from '../../shared/services/url.service';

@Component({
  selector: 'app-url-info',
  templateUrl: './url-info.component.html',
  styleUrls: ['./url-info.component.scss']
})
export class UrlInfoComponent implements OnInit {
  url?: ShortenedUrl;

  constructor(private route: ActivatedRoute, private urlService: UrlService) { }

  ngOnInit() {
    const shortCode = this.route.snapshot.paramMap.get('shortCode')!;
    this.urlService.getAllUrls().subscribe(urls => {
      this.url = urls.find(url => url.shortCode === shortCode);
    });
  }
}
