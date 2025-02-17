import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AboutService {
  private apiUrl = 'https://localhost:7234/api/About';

  constructor(private http: HttpClient) { }

  getDescription(): Observable<{ description: string }> {
    return this.http.get<{ description: string }>(`${this.apiUrl}/description`);
  }

  updateDescription(description: string): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/description`, { description });
  }
}
