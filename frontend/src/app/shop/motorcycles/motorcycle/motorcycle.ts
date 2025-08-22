import { Component, DestroyRef, inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MotorcycleService } from '../motorcycle.service';
import { MakeService } from '../../makes/make.service';
import { MyAccountService } from '../../../my-account/my-account.service';
import { IVehicleDTO } from '../../../models/IVehicleDTO';
import { UserAccount } from '../../../models/UserAccount';
import { switchMap, map } from 'rxjs';
import { ImageService } from '../../images/image.service';

@Component({
  selector: 'app-motorcycle',
  imports: [],
  templateUrl: './motorcycle.html',
  styleUrl: './motorcycle.css'
})
export class Motorcycle implements OnInit {

  private route = inject(ActivatedRoute);
  private motorcycleService = inject(MotorcycleService);
  private makeService = inject(MakeService);
  private userAccountService = inject(MyAccountService);
  private imageService = inject(ImageService);
  private destroyRef = inject(DestroyRef);

  public vin!: string;
  public motorcycle!: IVehicleDTO;
  public userAccount!: UserAccount;
  public message: string | undefined;


  public slideIndex: number = 0;

  nextSlide() {

    if (this.motorcycle && this.motorcycle.images.length > 0) {
      this.slideIndex = (this.slideIndex + 1) % this.motorcycle.images.length;
    }

  }

  prevSlide() {
    if (this.motorcycle && this.motorcycle.images.length > 0) {
      this.slideIndex =
        (this.slideIndex - 1 + this.motorcycle.images.length) % this.motorcycle.images.length;
    }
  }

  public ngOnInit(): void {

    this.vin = this.route.snapshot.paramMap.get('vin')!;

    const subscription = this.motorcycleService.getMotorcycleByVin(this.vin).pipe(
      switchMap(motorcycle => this.makeService.getMakeById(motorcycle.makeId).pipe(
        map(make => ({...motorcycle, make: make.makeName}))
      )),
      switchMap(motorcycle => this.userAccountService.getUserById(motorcycle.userAccountId).pipe(
        map(userAccount => ({ ...motorcycle, userAccount: userAccount }))
      )),
      switchMap(motorcycle => this.imageService.getMotorcycleImagesByVin(motorcycle.vin).pipe(
        map(images => ({ ...motorcycle, images: images }))
      ))
      ).subscribe({
        next: (motorcycleWithMakeAndUserAccountAndImages) => {

          this.motorcycle = motorcycleWithMakeAndUserAccountAndImages;
          this.userAccount = motorcycleWithMakeAndUserAccountAndImages.userAccount;
        },
        error: (err) => { this.message = err.message }
      }
    );

    this.destroyRef.onDestroy(() => {  
      subscription.unsubscribe();
    });
  }

}
