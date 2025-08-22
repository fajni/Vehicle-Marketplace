import { inject, Injectable } from "@angular/core";
import { BackendURL } from "../../backendUrl";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { IVehicleDTO } from "../../models/IVehicleDTO";

@Injectable({providedIn: 'root'})
export class CarService {
    
    private url: string = BackendURL.https + '/cars';

    private httpClient = inject(HttpClient);


    public getCars(): Observable<IVehicleDTO[]> {
        
        return this.httpClient.get<IVehicleDTO[]>(`${this.url}`);
    }

    public getCarByVin(vin: string): Observable<IVehicleDTO> {

        return this.httpClient.get<IVehicleDTO>(`${this.url}/${vin}`);
    }

    public getUserCarsByUserId(userId: number): Observable<IVehicleDTO[]> {

        return this.httpClient.get<IVehicleDTO[]>(`${this.url}/users/${userId}`, { withCredentials: true })
    }

}