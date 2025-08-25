import { Component, DestroyRef, inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { SellMotorcycleService } from './sell-motorcycle.service';
import { LoginService } from '../../login/login.service';
import { Make } from '../../models/Make';
import { ImageService } from '../../shop/images/image.service';
import { MakeService } from '../../shop/makes/make.service';
import { forkJoin, map, Observable, switchMap } from 'rxjs';
import { Image } from '../../models/Image';
import { IVehicleDTO } from '../../models/IVehicleDTO';

@Component({
  selector: 'app-motorcycle',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './sell-motorcycle.html',
  styleUrl: './sell-motorcycle.css'
})
export class SellMotorcycle implements OnInit{

  private sellMotorcycleService = inject(SellMotorcycleService);
  private imageService = inject(ImageService);
  private makeService = inject(MakeService);
  private loginService = inject(LoginService);
  private destroyRef = inject(DestroyRef);
  private router = inject(Router);

  public makes: Make[] = [];
  public message: string = "";

  public sellMotorcycleForm = new FormGroup({
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


  public postNewMotorcycle() {
    
    var extractedMake: string = "";
    var extractedImages: Image[] = [];

    for(let i = 0; i < this.makes.length; i++) {
      if(this.sellMotorcycleForm.value.makeId == this.makes[i].id){
        extractedMake = this.makes[i].makeName;
      }
    }

    extractedImages = this.sellMotorcycleForm.value.images!
      .split('\n')
      .map(url => url.trim())
      .filter(url => url.length > 0)
      .map(url => ({
        src: url,
        motorcycleVin: this.sellMotorcycleForm.value.vin!
      }))

    const motorcycle: IVehicleDTO = {
      vin: this.sellMotorcycleForm.value.vin!,
      name: this.sellMotorcycleForm.value.name!,
      make: extractedMake,
      description: this.sellMotorcycleForm.value.description!,
      price: this.sellMotorcycleForm.value.price!,
      capacity: this.sellMotorcycleForm.value.capacity!,
      power: this.sellMotorcycleForm.value.power!,
      images: extractedImages,
      makeId: this.sellMotorcycleForm.value.makeId!,
      userAccountId: this.sellMotorcycleForm.value.userAccountId!
    };

    const subscription = this.sellMotorcycleService.addVehicle(motorcycle).pipe(
          
      /* 
        after you save the motorcycle, switchMap starts ?
        response in switchMap is the response from addVechile() 
      */
      switchMap((response) => {

        // map images into Observable list of calls, every image is send as individual HTTP request
        const imageRequests: Observable<string>[] = extractedImages.map(img => {
          const image: Image = {
            motorcycleVin: motorcycle.vin,
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
        console.log(error);
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
        this.sellMotorcycleForm.patchValue({ userAccountId: Number(fullResponse.testAuthorizationResponse.userAccountId) });
        
      },
      error: (error) => {
        this.message = error;
      }
    })

    this.destroyRef.onDestroy(() => { subscription.unsubscribe(); });
  }
}
