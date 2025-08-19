import { Component, DestroyRef, inject, OnInit } from '@angular/core';
import { MotorcycleService } from './motorcycle.service';
import { IVehicleDTO } from '../../models/IVehicleDTO';
import { Make } from '../../models/Make';
import { forkJoin, map, switchMap } from 'rxjs';
import { MakeService } from '../makes/make.service';

@Component({
  selector: 'app-motorcycles',
  imports: [],
  templateUrl: './motorcycles.html',
  styleUrl: './motorcycles.css'
})
export class Motorcycles implements OnInit{

  private motorcycleService = inject(MotorcycleService);
  private makeService = inject(MakeService);
  private destroyRef = inject(DestroyRef);

  public motorcycles: IVehicleDTO[] = [];
  public makes: Make[] = [];

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
          next: (motorcycleWithMakes) => { this.motorcycles = motorcycleWithMakes; },
          error: (error) => console.error(error)
        });
    
        this.destroyRef.onDestroy(() => { subscription.unsubscribe(); });

  }

}
