import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Make } from "../../models/Make";
import { BackendURL } from "../../backendUrl";

@Injectable({providedIn: 'root'})
export class MakeService {

    private url: string = BackendURL.https + '/makes';

    private httpClient = inject(HttpClient);

    
    public getMakes(): Observable<Make[]>{

        return this.httpClient.get<Make[]>(`${this.url}`);
    }

    public getMakeById(id: number): Observable<Make> {

        return this.httpClient.get<Make>(`${this.url}/${id}`);
    }

    public addMake(make: Make): Observable<string> {

        return this.httpClient.post<string>(`${this.url}/add`, make, { withCredentials: true });
    }

    public deleteMake(id: number): Observable<string> {

        return this.httpClient.delete<string>(`${this.url}/delete/${id}`, { withCredentials: true });
    }

    public updateMake(id: number, updatedMake: Make): Observable<string> {

        return this.httpClient.put<string>(`${this.url}/update/${id}`, updatedMake, { withCredentials: true })
    }

}