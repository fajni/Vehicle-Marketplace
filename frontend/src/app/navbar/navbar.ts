import { Component, DestroyRef, inject, OnInit } from '@angular/core';
import { NavigationEnd, Router, RouterLink } from '@angular/router';
import { filter, switchMap } from 'rxjs';
import { LoginService } from '../login/login.service';

interface CheckStatunResponse {
  loggedIn: boolean;
  userEmail?: string;
  message: string;
}

@Component({
  selector: 'app-navbar',
  imports: [RouterLink],
  templateUrl: './navbar.html',
  styleUrl: './navbar.css'
})
export class Navbar implements OnInit{

  private router = inject(Router);
  private loginService = inject(LoginService);
  private destroyRef = inject(DestroyRef);

  public userLoggedIn: boolean = false;

  public logout() {
    
    const result = confirm("Are you sure?");

    if(result) {
      const subscription = this.loginService.logout().subscribe({
        next: (response) => {
          console.log('Successfully logged out!');
          window.location.reload();
        },
        error: (error) => { console.log(error); }
      });

      this.destroyRef.onDestroy(() => { subscription.unsubscribe(); })
    }

  }

  public showCars() {
    this.router.navigate(['/cars']);
  }

  public showMotorcycles() {
    this.router.navigate(['/motorcycles']);
  }

  ngOnInit(): void {

    const routerSubscription = this.router.events.pipe(
      filter(event => event instanceof NavigationEnd),
      switchMap(() => this.loginService.testAuthorization()) // automatski otkazuje prethodni request
    ).subscribe({
      next: (response) => {
        this.userLoggedIn = response.loggedIn === true;
      },
      error: (error) => console.log(error)
    });

    this.destroyRef.onDestroy(() => { routerSubscription.unsubscribe(); });
  }

}

