import { Component, DestroyRef, inject, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { LoginService } from '../login/login.service';

@Component({
  selector: 'app-shop',
  imports: [RouterLink],
  templateUrl: './shop.html',
  styleUrl: './shop.css'
})
export class Shop implements OnInit {
  
  public isUserLoggedIn: boolean = false;

  private loginService = inject(LoginService);
  private destroyRef = inject(DestroyRef);

  ngOnInit(): void {

    const subscription = this.loginService.testAuthorization().subscribe({
      next: (response) => {
        if(response.loggedIn === true) {
          this.isUserLoggedIn = true;
        }
      },
      error: (error) => { console.log(error); }
    });

    this.destroyRef.onDestroy(() => { subscription.unsubscribe(); });

  }



}
