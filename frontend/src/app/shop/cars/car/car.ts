import { Component, DestroyRef, inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CarService } from '../car.service';
import { IVehicleDTO } from '../../../models/IVehicleDTO';
import { MakeService } from '../../makes/make.service';
import { map, switchMap } from 'rxjs';
import { UserAccount } from '../../../models/UserAccount';
import { MyAccountService } from '../../../my-account/my-account.service';
import { ImageService } from '../../images/image.service';

@Component({
  selector: 'app-car',
  imports: [],
  templateUrl: './car.html',
  styleUrl: './car.css'
})
export class Car implements OnInit {

  private route = inject(ActivatedRoute);
  private carService = inject(CarService);
  private makeService = inject(MakeService);
  private userAccountService = inject(MyAccountService);
  private imageService = inject(ImageService);
  private destroyRef = inject(DestroyRef);

  public vin!: string;
  public car!: IVehicleDTO;
  public userAccount!: UserAccount;

  public message: string | undefined;

  public slideIndex: number = 0;

  nextSlide() {

    if (this.car && this.car.images.length > 0) {
      this.slideIndex = (this.slideIndex + 1) % this.car.images.length;
    }

  }

  prevSlide() {
    if (this.car && this.car.images.length > 0) {
      this.slideIndex =
        (this.slideIndex - 1 + this.car.images.length) % this.car.images.length;
    }
  }
  
  public ngOnInit(): void {

    this.vin = this.route.snapshot.paramMap.get('vin')!;

    const subscription = this.carService.getCarByVin(this.vin).pipe(
      switchMap(car => this.makeService.getMakeById(car.makeId).pipe(
        map(make => ({...car, make: make.makeName}))
      )),
      switchMap(car => this.userAccountService.getUserById(car.userAccountId).pipe(
        map(userAccount => ({ ...car, userAccount: userAccount }))
      )),
      switchMap(car => this.imageService.getCarImagesByVin(car.vin).pipe(
        map(images => ({ ...car, images: images }))
      ))
      ).subscribe({
        next: (carWithMakeAndUserAccountAndImages) => { 

          this.car = carWithMakeAndUserAccountAndImages;
          this.userAccount = carWithMakeAndUserAccountAndImages.userAccount;

        },
        error: (err) => { this.message = err.message }
      }
    );

    this.destroyRef.onDestroy(() => {  
      subscription.unsubscribe();
    });
  }

}
