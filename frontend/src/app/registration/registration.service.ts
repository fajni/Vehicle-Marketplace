import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { UserAccount } from "../models/UserAccount";
import { BackendURL } from "../backendUrl";

interface RegistrationResponse {
    message: string;
}

@Injectable({
    providedIn: 'root'
})
export class RegistrationService {

    private url: string = BackendURL.https;

    private httpClient = inject(HttpClient);


    public registerUser(user: UserAccount) {

        return this.httpClient.post<RegistrationResponse>(`${this.url}/users/registration`, user);
    }
}