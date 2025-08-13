import { inject, Injectable } from '@angular/core';
import { UserAccount } from "../models/UserAccount";
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface LoginResponse {
    message: string;
    cookie: string;
    //cookieValue: string;
    role: string;
}

interface TestAuthorization {
    loggedIn: boolean;
    message: string;
    userEmail?: string;
}

@Injectable({
    providedIn: 'root'
})
export class LoginService {

    private url: string = 'https://localhost:7000/api';

    private httpClient = inject(HttpClient);

    public getUsers(): Observable<UserAccount[]> {

        return this.httpClient.get<UserAccount[]>(`${this.url}/users`);
        // return this.httpClient.get<{ userAccounts: UserAccount[] }>(`${this.url}/users`);
    }

    public login(email: string, password: string): Observable<LoginResponse> {

        return this.httpClient.post<LoginResponse>(`${this.url}/users/login?email=${email}&password=${password}`, { email, password}, { withCredentials: true } );
    }

    public logout() {
        return this.httpClient.get(`${this.url}/users/logout`, { withCredentials: true });
    }


    public testAuthorization() {

        return this.httpClient.get<TestAuthorization>(`${this.url}/users/status`, { withCredentials: true });
    }
}