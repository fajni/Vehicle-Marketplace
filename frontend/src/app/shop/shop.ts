import { Component, DestroyRef, inject, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { LoginService } from '../login/login.service';
import { MakeService } from './makes/make.service';
import { Cars } from "./cars/cars";
import { Motorcycles } from "./motorcycles/motorcycles";

@Component({
  selector: 'app-shop',
  imports: [Cars, Motorcycles],
  templateUrl: './shop.html',
  styleUrl: './shop.css'
})
export class Shop implements OnInit {
  
  public isUserLoggedIn: boolean = false;
  public selected: string[] = [];
  public sort: string = "Price";

  private loginService = inject(LoginService);
  private makeService = inject(MakeService);
  private destroyRef = inject(DestroyRef);

  public onCheckboxChange(event: Event) {

    const inputHTML = event.target as HTMLInputElement;

    if (inputHTML.checked) {
      
      if (!this.selected.includes(inputHTML.value)) {
        this.selected.push(inputHTML.value);
      }

    }
    else {
      this.selected = this.selected.filter(v => v !== inputHTML.value);
    }

    if (this.selected.length === 0) {
      console.log('Nothing selected');
    } else {
      console.log('Selected:', this.selected.join(', '));
    }

  }

  public onSortChange(event: Event) {

    const select = event.target as HTMLSelectElement;
    this.sort = select.value;
  }

  ngOnInit(): void {

    const subscriptionAuthorization = this.loginService.testAuthorization().subscribe({
      next: (response) => {
        if(response.loggedIn === true) {
          this.isUserLoggedIn = true;
        }
      },
      error: (error) => { console.log(error); }
    });

    const subscriptionMake = this.makeService.getMakes().subscribe({
      next: (response) => {  },
      error: (error) => { console.log(error); }
    });

    this.destroyRef.onDestroy(() => {

      subscriptionAuthorization.unsubscribe();
      subscriptionMake.unsubscribe();

    });

  }

}
