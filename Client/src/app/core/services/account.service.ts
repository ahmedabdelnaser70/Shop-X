import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Address, User } from '../../shared/models/user';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  baseUrl = environment.baseUrl;
  private http = inject(HttpClient);
  currentUser = signal<User | null>(null);

  login(valuse: any) {
    let params = new HttpParams();
    params = params.append('useCookies', 'true');
    return this.http.post<User>(`${this.baseUrl}login`, valuse, { params });
  }

  register(valuse: any) {
    return this.http.post(`${this.baseUrl}account/register`, valuse);
  }

  getUserInfo() {
    return this.http.get<User>(`${this.baseUrl}account/user-info`).pipe(
      map((user) => {
        this.currentUser.set(user);
        return user;
      }),
    );
  }

  logout() {
    return this.http.post(`${this.baseUrl}account/logout`, {});
  }

  updateAddress(address: Address) {
    return this.http.post(`${this.baseUrl}account/address`, address);
  }

  getAuthStatus() {
    return this.http.get<{ isAuthenticated: boolean }>(`${this.baseUrl}account/auth-status`);
  }
}
