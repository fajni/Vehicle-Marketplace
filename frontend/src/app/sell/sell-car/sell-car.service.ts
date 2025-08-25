import { inject, Injectable } from "@angular/core";
import { BackendURL } from "../../backendUrl";
import { IVehicleDTO } from "../../models/IVehicleDTO";
import { Observable } from "rxjs";
import { HttpClient } from "@angular/common/http";

@Injectable({
    providedIn: 'root'
})
export class SellCarService {

    private url: string = BackendURL.https + "/cars";

    private httpClient = inject(HttpClient);

    // sellVehicle()
    public addVehicle(newCar: IVehicleDTO): Observable<string> {

        return this.httpClient.post<string>(`${this.url}/add`, newCar, {withCredentials: true});
    }

}