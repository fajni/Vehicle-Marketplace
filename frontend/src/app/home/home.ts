import { Component, DestroyRef, inject, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { LoginResponse, LoginService } from '../login/login.service';

@Component({
  selector: 'app-home',
  imports: [RouterLink],
  templateUrl: './home.html',
  styleUrl: './home.css'
})
export class Home implements OnInit{

  private loginService = inject(LoginService);
  private destroyRef = inject(DestroyRef);

  public showWelcomeBackText: boolean = false;
  public user?: string = "";


  ngOnInit(): void {

    const subscription = this.loginService.testAuthorization().subscribe({
      next: (response) => {

        if(response.loggedIn == true) {
          this.showWelcomeBackText = true;
          this.user = response.userEmail;
        }

      },
      error: (error) => { console.log(error) }
    });

    this.destroyRef.onDestroy(() => { subscription.unsubscribe(); });

  }

}
