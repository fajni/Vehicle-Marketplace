import { IVehicleDTO } from "./IVehicleDTO";

export class Make {

    id!: number;
    makeName!: string;

    cars?: IVehicleDTO[];
    motorcycles?: IVehicleDTO[];

    constructor(id: number, makeName: string, cars?: IVehicleDTO[], motorcycles?: IVehicleDTO[]) {

        this.id = id;
        this.makeName = makeName;

        this.cars = cars;
        this.motorcycles = motorcycles;
    }

}