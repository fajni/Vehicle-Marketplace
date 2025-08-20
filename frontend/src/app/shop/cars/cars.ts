import { Component, DestroyRef, inject, Input, OnInit } from '@angular/core';
import { CarService } from './car.service';
import { IVehicleDTO } from '../../models/IVehicleDTO';
import { Make } from '../../models/Make';
import { MakeService } from '../makes/make.service';
import { forkJoin, map, switchMap } from 'rxjs';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-cars',
  imports: [RouterLink],
  templateUrl: './cars.html',
  styleUrl: './cars.css'
})
export class Cars implements OnInit{

  private carService = inject(CarService);
  private makeService = inject(MakeService);
  private destroyRef = inject(DestroyRef);

  @Input({required: true}) public sort!: string;

  public cars: IVehicleDTO[] = [];
  public makes: Make[] = [];

  public ngOnInit(): void {
    
    const subscription = this.carService.getCars().pipe(
      switchMap(cars => {
        // create array Observable for every car.makeId
        const makeRequests = cars.map(car => 
          this.makeService.getMakeById(car.makeId).pipe(
            map(make => ({ ...car, make: make.makeName }))
          )
        );

      // start all requests parallel
      return forkJoin(makeRequests);
    })
    ).subscribe({
      next: (carsWithMakes) => { 
        
        this.cars = carsWithMakes;

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
