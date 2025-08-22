import { Image } from "./Image";

export interface IVehicleDTO {
    
    vin: string;
    name: string;
    make: string;
    description: string;
    price: number;
    capacity: number;
    power: number;

    images: Image[];

    makeId: number;
    userAccountId: number;
}