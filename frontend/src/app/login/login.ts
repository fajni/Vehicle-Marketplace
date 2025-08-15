import { Component, DestroyRef, inject, model, OnInit, signal } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { UserAccount } from '../models/UserAccount';
import { LoginService } from './login.service';
import { PopupMessage } from "../popup-message/popup-message";

@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule, RouterLink, PopupMessage],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login implements OnInit {

  get invalidEmail() {
    return (
      this.loginForm.controls.email.touched &&
      this.loginForm.controls.email.dirty &&
      this.loginForm.controls.email.invalid
    );
  }

  get invalidPassword() {
    return (
      this.loginForm.controls.password.touched &&
      this.loginForm.controls.password.dirty &&
      this.loginForm.controls.password.invalid
    );
  }

  private loginService = inject(LoginService);
  private destroyRef = inject(DestroyRef);
  private router = inject(Router);

  public nullValues: boolean = false;
  public successfulLogin: boolean | undefined;
  public loginMessage: string = "";
  public alreadyLogin: boolean = false;

  public loginForm = new FormGroup({
    email: new FormControl('', { validators: [ Validators.email, Validators.required ] }),
    password: new FormControl('', { validators: [ Validators.minLength(5), Validators.required ] })
  });


  // LOGIN
  public onSubmit() {

    if(this.loginForm.valid) {

      const { email, password } = this.loginForm.value;

      const subscription = this.loginService.login(email!, password!).subscribe({
        next: (response) => { 
          console.log('Login successful! ');
          console.log(response);
          this.successfulLogin = true;

          var cookie: string = response.cookie.toString();
          //console.log("Cookie" + cookie);
          
          const cookieValue = cookie.split('=')[1].split(';')[0];
          //console.log("Cookie value: " + cookieValue);

          this.router.navigate(['/home']);

        },
        error: (error: Error) => { 
          console.log('Login failed!'); 
          console.log(error);
          this.loginMessage = error.message;
          this.successfulLogin = false;
        }
      });

      this.destroyRef.onDestroy(() => { subscription.unsubscribe() });
    }
    else {
      this.nullValues = true;
    }
  }

  // DELETE THIS METHOD! (THIS IS JUST FOR TESTING PURPOSES ONLY)
  public ngOnInit(): void {

    const subscription = this.loginService.getUsers().subscribe({
      next: (users) => {

        for(var user of users as UserAccount[]) {
          console.table(user);
        }

      },
      error: (error: Error) => { console.log(error) },
      complete: () => { console.log('Data fetched!') }
    });

    const subscription2 = this.loginService.testAuthorization().subscribe({
      next: (response) => {
        if(response.loggedIn == true) {
          this.alreadyLogin = true;
        }
      },
    });

    this.destroyRef.onDestroy(() => {
      subscription.unsubscribe();
    });

  }


  public testAuthorizationButton() {
    
    const subscription = this.loginService.testAuthorization().subscribe({
      next: (response) => { console.log(response); },
      error: (error) => { console.log(error); }
    });

    this.destroyRef.onDestroy(() => {subscription.unsubscribe();});
  }

}
