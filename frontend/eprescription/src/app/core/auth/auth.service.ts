import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { TokenStorageService } from './token-storage.service';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private apiUrl = 'https://localhost:7096/api/Account'; // 🔹 replace with your backend

  constructor(
    private http: HttpClient,
    @Inject(TokenStorageService) private tokenStorage: TokenStorageService
  ) {}

  login(credentials: { username: string; password: string }): Observable<any> {
    return this.http.post<{ token: string }>(`${this.apiUrl}/login`, credentials).pipe(
      tap(response => {
        this.tokenStorage.saveToken(response.token);
      })
    );
  }

  logout() {
    this.tokenStorage.clearToken();
  }

  isAuthenticated(): boolean {
    return !!this.tokenStorage.getToken();
  }
}
