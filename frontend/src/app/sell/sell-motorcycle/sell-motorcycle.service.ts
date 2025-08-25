import { inject, Injectable } from "@angular/core";
import { BackendURL } from "../../backendUrl";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { IVehicleDTO } from "../../models/IVehicleDTO";

@Injectable({
    providedIn: 'root'
})
export class SellMotorcycleService {

    private url: string = BackendURL.https + "/motorcycles";

    private httpClient = inject(HttpClient);

    // sellVehicle()
    public addVehicle(newMotorcycle: IVehicleDTO): Observable<string> {

        return this.httpClient.post<string>(`${this.url}/add`, newMotorcycle, {withCredentials: true});
    }

}