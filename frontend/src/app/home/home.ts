import { AfterViewInit, Component, DestroyRef, inject, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { LoginService } from '../login/login.service';
import { PopupMessage } from "../popup-message/popup-message";

@Component({
  selector: 'app-home',
  imports: [RouterLink, PopupMessage],
  templateUrl: './home.html',
  styleUrl: './home.css'
})
export class Home implements OnInit{

  private loginService = inject(LoginService);
  private destroyRef = inject(DestroyRef);

  public showWelcomeBackText: boolean | undefined;
  public user?: string = "";


  ngOnInit(): void {

    const subscription = this.loginService.testAuthorization().subscribe({
      next: (response) => {

        if(response.loggedIn == true) {
          this.user = `${response.firstname} ${response.lastname}`;

          const img = document.querySelector('header img');
          
          if(img) {
            img.addEventListener('load', () => {
              this.showWelcomeBackText = true; // this will show only 1 time, because Angular will render component only 1 time
            })
          }

        }
        else {

          const img = document.querySelector('header img');
          
          if(img) {
            img.addEventListener('load', () => {
              this.showWelcomeBackText = false; // this will show only 1 time, because Angular will render component only 1 time
            })
          }
          
        }

      },
      error: (error) => { console.log(error) }
    });

    this.destroyRef.onDestroy(() => { subscription.unsubscribe(); });

  }

}
