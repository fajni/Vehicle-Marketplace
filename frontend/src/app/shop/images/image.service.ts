import { inject, Injectable } from "@angular/core";
import { BackendURL } from "../../backendUrl";
import { HttpClient } from "@angular/common/http";
import { Image } from "../../models/Image";
import { Observable } from "rxjs";

@Injectable({providedIn: 'root'})
export class ImageService {

    private url: string = BackendURL.https + '/images';

    private httpClient = inject(HttpClient);

    public getAllImages() {

        return this.httpClient.get<Image[]>(`${this.url}`);
    }

    public getCarImagesByVin(vin: string): Observable<Image[]> {

        return this.httpClient.get<Image[]>(`${this.url}/cars/${vin}`);
    }

    public getMotorcycleImagesByVin(vin: string) {

        return this.httpClient.get<Image[]>(`${this.url}/motorcycles/${vin}`);
    }

}