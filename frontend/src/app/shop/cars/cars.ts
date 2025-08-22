import { Component, DestroyRef, inject, Input, OnInit } from '@angular/core';
import { CarService } from './car.service';
import { IVehicleDTO } from '../../models/IVehicleDTO';
import { Make } from '../../models/Make';
import { MakeService } from '../makes/make.service';
import { forkJoin, map, switchMap } from 'rxjs';
import { RouterLink } from '@angular/router';
import { ImageService } from '../images/image.service';

@Component({
  selector: 'app-cars',
  imports: [RouterLink],
  templateUrl: './cars.html',
  styleUrl: './cars.css'
})
export class Cars implements OnInit{

  private carService = inject(CarService);
  private makeService = inject(MakeService);
  private imageService = inject(ImageService);
  private destroyRef = inject(DestroyRef);

  @Input({required: true}) public sort!: string;

  public cars: IVehicleDTO[] = [];

  public ngOnInit(): void {
    
    const subscription = this.carService.getCars().pipe(
      switchMap(cars => this.makeService.getMakes().pipe(
        map( makes => ({ cars, makes }) )
      )),
      switchMap(cars => this.imageService.getAllImages().pipe(
        map( images => ({ ...cars, images }) )
      ))
    ).subscribe({
      next: (fullResponse) => {

        for(let i = 0; i < fullResponse.cars.length; i++) {
          
          const car = fullResponse.cars[i];

          const make = fullResponse.makes.find(m => m.id === car.makeId);
          if(make)
            car.make = make.makeName;

          car.images = fullResponse.images.filter(img => img.carVin === car.vin);

          this.cars.push(car);
        }

        switch(this.sort) {
          
          case 'Price':
            this.cars.sort((a, b) => a.price - b.price);
            break;
          
          case 'Power':
            this.cars.sort((a, b) => a.power - b.power);
            break;
          
          case 'Capacity':
            this.cars.sort((a, b) => a.capacity - b.capacity);
            break;
          
          default:
            break;
        }

      },
      error: (error) => console.error(error)
    });

    this.destroyRef.onDestroy(() => { subscription.unsubscribe(); });

  }

}
