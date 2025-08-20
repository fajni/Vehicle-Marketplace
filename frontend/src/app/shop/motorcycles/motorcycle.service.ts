import { inject, Injectable } from "@angular/core";
import { BackendURL } from "../../backendUrl";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { IVehicleDTO } from "../../models/IVehicleDTO";

@Injectable({providedIn: 'root'})
export class MotorcycleService {
    
    private url: string = BackendURL.https + '/motorcycles';

    private httpClient = inject(HttpClient);


    public getMotorcycles(): Observable<IVehicleDTO[]> {
        
        return this.httpClient.get<IVehicleDTO[]>(`${this.url}`);
    }

    public getMotorcycleByVin(vin: string): Observable<IVehicleDTO> {

        return this.httpClient.get<IVehicleDTO>(`${this.url}/${vin}`);
    }

}