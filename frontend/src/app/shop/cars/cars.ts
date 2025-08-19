import { Component, DestroyRef, inject, OnInit } from '@angular/core';
import { CarService } from './car.service';
import { IVehicleDTO } from '../../models/IVehicleDTO';
import { Make } from '../../models/Make';
import { MakeService } from '../makes/make.service';
import { forkJoin, map, switchMap } from 'rxjs';

@Component({
  selector: 'app-cars',
  imports: [],
  templateUrl: './cars.html',
  styleUrl: './cars.css'
})
export class Cars implements OnInit{

  private carService = inject(CarService);
  private makeService = inject(MakeService);
  private destroyRef = inject(DestroyRef);

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
      next: (carsWithMakes) => { this.cars = carsWithMakes; },
      error: (error) => console.error(error)
    });

    this.destroyRef.onDestroy(() => { subscription.unsubscribe(); });

  }

}
