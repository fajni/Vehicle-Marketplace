import { Component, DestroyRef, inject, Input, OnInit } from '@angular/core';
import { MotorcycleService } from './motorcycle.service';
import { IVehicleDTO } from '../../models/IVehicleDTO';
import { Make } from '../../models/Make';
import { forkJoin, map, switchMap } from 'rxjs';
import { MakeService } from '../makes/make.service';
import { RouterLink } from '@angular/router';
import { ImageService } from '../images/image.service';

@Component({
  selector: 'app-motorcycles',
  imports: [RouterLink],
  templateUrl: './motorcycles.html',
  styleUrl: './motorcycles.css'
})
export class Motorcycles implements OnInit{

  private motorcycleService = inject(MotorcycleService);
  private makeService = inject(MakeService);
  private imageService = inject(ImageService);
  private destroyRef = inject(DestroyRef);

  public motorcycles: IVehicleDTO[] = [];
  public makes: Make[] = [];

  @Input({required: true}) public sort!: string;

  public ngOnInit(): void {
    
    const subscription = this.motorcycleService.getMotorcycles().pipe(
      switchMap(motorcycles => this.makeService.getMakes().pipe(
        map(makes => ({motorcycles, makes}))
      )),
      switchMap(motorcycles => this.imageService.getAllImages().pipe(
        map(images => ({...motorcycles, images}))
      ))
      ).subscribe({
        next: (fullResponse) => { 

          for(let i = 0; i < fullResponse.motorcycles.length; i++) {

            const motorcycle = fullResponse.motorcycles[i];

            const make = fullResponse.makes.find(m => m.id === motorcycle.makeId);
            if(make)
              motorcycle.make = make.makeName;

            motorcycle.images = fullResponse.images.filter(img => img.motorcycleVin === motorcycle.vin);

            this.motorcycles.push(motorcycle);
          }

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
