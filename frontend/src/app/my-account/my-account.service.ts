import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { map, Observable } from "rxjs";
import { UserAccount } from "../models/UserAccount";
import { BackendURL } from "../backendUrl";

@Injectable({providedIn: 'root'})
export class MyAccountService {

    private url: string = BackendURL.https;

    private httpClient = inject(HttpClient);

    public getUserById(id: number): Observable<UserAccount> {
        return this.httpClient.get<UserAccount>(`${this.url}/users/${id}`, { withCredentials: true });
    }

    public getUserByEmail(email: string): Observable<UserAccount | undefined>{
        return this.httpClient.get<UserAccount[]>(`${this.url}/users`, { withCredentials: true })
            .pipe(map(users => users.find(user => user.email === email)));
    }

    public updateUserAccount(id: number, updatedUser: UserAccount) {
        return this.httpClient.put<string>(`${this.url}/users/update/${id}`, updatedUser, { withCredentials: true });
    }

    public deleteUserAccount(id: number) {
        return this.httpClient.delete<string>(`${this.url}/users/delete/${id}`, { withCredentials: true });
    }
}