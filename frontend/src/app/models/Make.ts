import { Car } from "./Car";
import { Motorcycle } from "./Motorcycle";

export class Make {

    id!: number;
    makeName!: string;

    cars?: Car[];
    motorcycles?: Motorcycle[];

    constructor(id: number, makeName: string, cars?: Car[], motorcycles?: Motorcycle[]) {

        this.id = id;
        this.makeName = makeName;

        this.cars = cars;
        this.motorcycles = motorcycles;
    }

}