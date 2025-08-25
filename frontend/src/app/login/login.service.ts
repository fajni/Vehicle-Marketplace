import { inject, Injectable } from '@angular/core';
import { UserAccount } from "../models/UserAccount";
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ILoginResponse } from '../models/ILoginResponse';
import { ITestAuthorization } from '../models/ITestAuthorization';
import { BackendURL } from '../backendUrl';

@Injectable({
    providedIn: 'root'
})
export class LoginService {

    private url: string = BackendURL.https;

    private httpClient = inject(HttpClient);

    public getUsers(): Observable<UserAccount[]> {

        // return this.httpClient.get<{ userAccounts: UserAccount[] }>(`${this.url}/users`);
        return this.httpClient.get<UserAccount[]>(`${this.url}/users`);
    }

    public login(email: string, password: string): Observable<ILoginResponse> {

        return this.httpClient.post<ILoginResponse>(`${this.url}/users/login?email=${email}&password=${password}`, { email, password}, { withCredentials: true } );
    }

    public logout() {
        
        return this.httpClient.get(`${this.url}/users/logout`, { withCredentials: true });
    }


    public testAuthorization(): Observable<ITestAuthorization> {

        return this.httpClient.get<ITestAuthorization>(`${this.url}/users/status`, { withCredentials: true });
    }
}