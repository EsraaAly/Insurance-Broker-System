import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private isAuthenticatedSubject = new BehaviorSubject<boolean>(false);
  private currentUserSubject = new BehaviorSubject<any>(null);

  isAuthenticated$ = this.isAuthenticatedSubject.asObservable();
  currentUser$ = this.currentUserSubject.asObservable();

  constructor() {
    // Check if user is logged in on app startup
    const token = localStorage.getItem('authToken');
    const user = localStorage.getItem('currentUser');
    
    if (token && user) {
      this.isAuthenticatedSubject.next(true);
      this.currentUserSubject.next(JSON.parse(user));
    }
  }

  login(credentials: { username: string; password: string }): Observable<boolean> {
    // This is a mock login - replace with actual API call
    return new Observable(observer => {
      // Simulate API call
      setTimeout(() => {
        if (credentials.username === 'admin' && credentials.password === 'password') {
          const user = {
            id: 1,
            username: 'admin',
            email: 'admin@insurancebroker.com',
            role: 'Administrator'
          };
          
          const token = 'mock-jwt-token';
          
          localStorage.setItem('authToken', token);
          localStorage.setItem('currentUser', JSON.stringify(user));
          
          this.isAuthenticatedSubject.next(true);
          this.currentUserSubject.next(user);
          
          observer.next(true);
          observer.complete();
        } else {
          observer.next(false);
          observer.complete();
        }
      }, 1000);
    });
  }

  logout(): void {
    localStorage.removeItem('authToken');
    localStorage.removeItem('currentUser');
    
    this.isAuthenticatedSubject.next(false);
    this.currentUserSubject.next(null);
  }

  get currentUserValue(): any {
    return this.currentUserSubject.value;
  }

  get isAuthenticatedValue(): boolean {
    return this.isAuthenticatedSubject.value;
  }

  get authToken(): string | null {
    return localStorage.getItem('authToken');
  }
}
