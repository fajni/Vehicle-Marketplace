import { Component, DestroyRef, inject, Input, OnInit } from '@angular/core';
import { MotorcycleService } from './motorcycle.service';
import { IVehicleDTO } from '../../models/IVehicleDTO';
import { Make } from '../../models/Make';
import { forkJoin, map, switchMap } from 'rxjs';
import { MakeService } from '../makes/make.service';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-motorcycles',
  imports: [RouterLink],
  templateUrl: './motorcycles.html',
  styleUrl: './motorcycles.css'
})
export class Motorcycles implements OnInit{

  private motorcycleService = inject(MotorcycleService);
  private makeService = inject(MakeService);
  private destroyRef = inject(DestroyRef);

  public motorcycles: IVehicleDTO[] = [];
  public makes: Make[] = [];

  @Input({required: true}) public sort!: string;

  public ngOnInit(): void {
    
    const subscription = this.motorcycleService.getMotorcycles().pipe(
      switchMap(motorcycles => {
        // create array Observable for every motorcycle.makeId
        const makeRequests = motorcycles.map(motorcycle => 
          this.makeService.getMakeById(motorcycle.makeId).pipe(
            map(make => ({ ...motorcycle, make: make.makeName }))
          )
        );
    
        // start all requests parallel
        return forkJoin(makeRequests);
        })
      ).subscribe({
        next: (motorcycleWithMakes) => { 

          this.motorcycles = motorcycleWithMakes;

          switch(this.sort) {
          
            case 'Price':
              this.motorcycles.sort((a, b) => a.price - b.price);
              break;
          
            case 'Power':
              this.motorcycles.sort((a, b) => a.power - b.power);
              break;
          
            case 'Capacity':
              this.motorcycles.sort((a, b) => a.capacity - b.capacity);
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
