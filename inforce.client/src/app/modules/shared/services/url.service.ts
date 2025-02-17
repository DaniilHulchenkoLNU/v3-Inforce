import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ShortUrl } from '../../../models/ShortUrl';

export interface ShortenedUrl {
  id: number;
  originalUrl: string;
  shortCode: string;
  createdAt: string;
  createdBy: string;
}

@Injectable({
  providedIn: 'root'
})
export class UrlService {
  private apiUrl = 'https://localhost:7234/api/Url';

  constructor(private http: HttpClient) { }

  getAllUrls(): Observable<ShortenedUrl[]> {
    return this.http.get<ShortenedUrl[]>(`${this.apiUrl}/all`);
  }

  shortenUrl(originalUrl: string): Observable<ShortUrl> {
    return this.http.post<ShortUrl>(`${this.apiUrl}/shorten`, { originalUrl });
  }

  deleteUrl(shortCode: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/delete/${shortCode}`);
  }
}
