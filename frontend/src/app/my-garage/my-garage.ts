import { Component, DestroyRef, inject, OnInit } from '@angular/core';
import { LoginService } from '../login/login.service';
import { UserAccount } from '../models/UserAccount';
import { MyAccountService } from '../my-account/my-account.service';
import { map, switchMap } from 'rxjs';
import { IVehicleDTO } from '../models/IVehicleDTO';
import { MakeService } from '../shop/makes/make.service';
import { RouterLink } from '@angular/router';
import { ImageService } from '../shop/images/image.service';
import { Image } from '../models/Image';

@Component({
  selector: 'app-my-garage',
  imports: [],
  templateUrl: './my-garage.html',
  styleUrl: './my-garage.css'
})
export class MyGarage implements OnInit{

  private loginService = inject(LoginService);
  private accountService = inject(MyAccountService);
  private imageService = inject(ImageService);
  private makeService = inject(MakeService);
  private destroyRef = inject(DestroyRef);

  public account: UserAccount | undefined;
  public myVehicles: IVehicleDTO[] = [];
  public myImages: Image[] = [];

  public slideIndexes: { [vin: string]: number } = {};

  public nextSlide(vehicleVin: string, imagesLength: number) {
    
    if (!this.slideIndexes[vehicleVin]) 
      this.slideIndexes[vehicleVin] = 0;
    
    this.slideIndexes[vehicleVin] = (this.slideIndexes[vehicleVin] + 1) % imagesLength;
  }

  public prevSlide(vehicleVin: string, imagesLength: number) {

    if (!this.slideIndexes[vehicleVin]) 
      this.slideIndexes[vehicleVin] = 0;
    
    this.slideIndexes[vehicleVin] = (this.slideIndexes[vehicleVin] - 1 + imagesLength) % imagesLength;
  }
  
  public ngOnInit(): void {

    const subscription = this.loginService.testAuthorization().pipe(
      switchMap(response => this.accountService.getUserByEmail(response.userEmail!).pipe(
        map(account => ({...response, account}))
      )),
      switchMap(response => this.accountService.getUserAccountVehiclesByUserAccountId(response.account!.id).pipe(
        map(vehicles => ({ ...response, vehicles }))
      )),
      switchMap(response => this.makeService.getMakes().pipe(
        map(makes => ({ ...response, makes }))
      )),
      switchMap(response => this.imageService.getAllImages().pipe(
        map(images => {
          const vehiclesWithImages = response.vehicles.map(vehicle => ({
            ...vehicle, images: images.filter(img => img.carVin === vehicle.vin || img.motorcycleVin === vehicle.vin)
          }));
          
          return { ...response, vehicles: vehiclesWithImages, images };
        })
      ))
    ).subscribe({
      next: (response) => {

        this.account = response.account;

        this.myVehicles = response.vehicles;

        const allMakes = response.makes;

        for(let i = 0; i < allMakes.length; i++) {
          for(let j = 0; j < this.myVehicles.length; j++) {
            if(this.myVehicles[j].makeId == allMakes[i].id) {
              this.myVehicles[j].make = allMakes[i].makeName;
            }
          }
        }

        // inicijalizuj slideIndexes za svaki vin
        for (const vehicle of this.myVehicles) {
          if (vehicle.images?.length > 0) {
            this.slideIndexes[vehicle.vin] = 0;
          }
        }

      },
      error: (error) => { console.log(error); }
    });

    this.destroyRef.onDestroy(() => {
      subscription.unsubscribe();
    });

  }

}
