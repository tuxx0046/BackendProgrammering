import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { ApplicationUserCreate } from './application-user-create.model';
import { ApplicationUserLogin } from './application-user-login.model';
import { ApplicationUser } from './application-user.model';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private currentUserSubject$: BehaviorSubject<ApplicationUser>;

  constructor(private http: HttpClient) {
    this.currentUserSubject$ = new BehaviorSubject<ApplicationUser>(JSON.parse(<string>localStorage.getItem('currentUser')));
  }

  login(model: ApplicationUserLogin): Observable<ApplicationUser> {
    return this.http.post<ApplicationUser>('http://localhost:5000/api/account/login', model).pipe(
      map((user: ApplicationUser) => {

        if (user) {
          localStorage.setItem('currentUser', JSON.stringify(user));
          this.setCurrentUser(user);
        }

        return user;
      })
    )
  }

  register(model: ApplicationUserCreate): Observable<ApplicationUser> {
    return this.http.post<ApplicationUser>('http://localhost:5000/api/account/register', model).pipe(
      map((user: ApplicationUser) => {

        if (user) {
          localStorage.setItem('currentUser', JSON.stringify(user));
          this.setCurrentUser(user);
        }

        return user;
      })
    )
  }

  setCurrentUser(user: ApplicationUser) {
    this.currentUserSubject$.next(user);
  }

  public get currentUserValue(): ApplicationUser {
    return this.currentUserSubject$.value;
  }

  public givenUserIsLoggedIn(username: string) {
    return this.isLoggedIn() && this.currentUserValue.username === username;
  }

  public isLoggedIn() {
    const currentUser = this.currentUserValue;
    const isLoggedIn = !!currentUser && !!currentUser.token;
    return isLoggedIn;
  }

  logout() {
    localStorage.removeItem('currentUser');
    this.currentUserSubject$.next(null);
  }
}
