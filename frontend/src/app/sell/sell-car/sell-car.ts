import { Component, DestroyRef, inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MakeService } from '../../shop/makes/make.service';
import { LoginService } from '../../login/login.service';
import { forkJoin, map, Observable, switchMap } from 'rxjs';
import { Make } from '../../models/Make';
import { Router, RouterLink } from '@angular/router';
import { IVehicleDTO } from '../../models/IVehicleDTO';
import { SellCarService } from './sell-car.service';
import { Image } from '../../models/Image';
import { ImageService } from '../../shop/images/image.service';

@Component({
  selector: 'app-car',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './sell-car.html',
  styleUrl: './sell-car.css'
})
export class SellCar implements OnInit{

  private sellCarService = inject(SellCarService);
  private imageService = inject(ImageService);
  private makeService = inject(MakeService);
  private loginService = inject(LoginService);
  private destroyRef = inject(DestroyRef);
  private router = inject(Router);

  public makes: Make[] = [];
  public message: string = "";


  public sellCarForm = new FormGroup({
    vin: new FormControl<string>('', { validators: [Validators.required, Validators.minLength(17)] }),
    name: new FormControl<string>('', { validators: [Validators.required] }),
    description: new FormControl<string>('', { validators: [] }),
    price: new FormControl<number>(0, { validators: [Validators.required] }),
    capacity: new FormControl<number>(0, { validators: [Validators.required] }),
    power: new FormControl<number>(0, { validators: [Validators.required] }),

    images: new FormControl<string>('', { validators: [] }),

    makeId: new FormControl<number>(0, { validators: [Validators.required] }),
    userAccountId: new FormControl<number>(0, { validators: [Validators.required] })
  });

  public postNewCar() {

    var extractedMake: string = "";
    var extractedImages: Image[] = [];

    for(let i = 0; i < this.makes.length; i++) {
      if(this.sellCarForm.value.makeId == this.makes[i].id){
        extractedMake = this.makes[i].makeName;
      }
    }

    extractedImages = this.sellCarForm.value.images!
      .split('\n')
      .map(url => url.trim())
      .filter(url => url.length > 0)
      .map(url => ({
        src: url,
        carVin: this.sellCarForm.value.vin!
      }))

    const car: IVehicleDTO = {
      vin: this.sellCarForm.value.vin!,
      name: this.sellCarForm.value.name!,
      make: extractedMake,
      description: this.sellCarForm.value.description!,
      price: this.sellCarForm.value.price!,
      capacity: this.sellCarForm.value.capacity!,
      power: this.sellCarForm.value.power!,
      images: extractedImages,
      makeId: this.sellCarForm.value.makeId!,
      userAccountId: this.sellCarForm.value.userAccountId!
    };

    const subscription = this.sellCarService.addVehicle(car).pipe(
      
      /* 
        after you save the car, switchMap starts ?
        response in switchMap is the response from addVechile() 
      */
      switchMap((response) => {

        // map images into Observable list of calls, every image is send as individual HTTP request
        const imageRequests: Observable<string>[] = extractedImages.map(img => {
          const image: Image = {
            carVin: car.vin,
            src: img.src
          };

          // return value for Observable list
          return this.imageService.addImage(image);
        });

        // forkJoin starts all image requests parallel; emits 1 result
        // if 1 image fails, whole forkJoin fails
        return forkJoin(imageRequests);
      })

    )
    .subscribe({
      next: (response) => {
        this.message = response.toString();
        this.router.navigate(['/garage']);
      },
      error: (error) => { 
        this.message = error;
      }
    });

    this.destroyRef.onDestroy(() => subscription.unsubscribe());

  }


  public ngOnInit(): void {
    
    const subscription = this.loginService.testAuthorization().pipe(
      switchMap(testAuthorizationResponse => this.makeService.getMakes().pipe(
        map(makes => ({  testAuthorizationResponse, makes: makes }))
      ))
    ).subscribe({
      next: (fullResponse) => {

        this.makes = fullResponse.makes;
        this.sellCarForm.patchValue({ userAccountId: Number(fullResponse.testAuthorizationResponse.userAccountId) });
        
      },
      error: (error) => {
        this.message = error;
      }
    })

    this.destroyRef.onDestroy(() => { subscription.unsubscribe(); });
  }

}
