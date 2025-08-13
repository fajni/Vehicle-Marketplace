import { Component, DestroyRef, inject, model, OnInit, signal } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { UserAccount } from '../models/UserAccount';
import { LoginService } from './login.service';

@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule, RouterLink],
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

  public userAccounts = signal<UserAccount[]>([]); // delete and implemented in different way

  public nullValues: boolean = false;
  public successfulLogin: boolean | undefined;
  public loginMessage: string = "";

  public loginForm = new FormGroup({
    email: new FormControl('', { validators: [ Validators.email, Validators.required ] }),
    password: new FormControl('', { validators: [ Validators.minLength(5), Validators.required ] })
  });


  // LOGIN
  public onSubmit() {

    if(this.loginForm.valid) {

      console.log('Email: ' + this.loginForm.value.email);
      console.log('Password: ' + this.loginForm.value.password);

      const { email, password } = this.loginForm.value;

      const subscription = this.loginService.login(email!, password!).subscribe({
        next: (response) => { 
          //console.log('Login successful! ');
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
        this.userAccounts.set(users)

        for(var user of this.userAccounts() as UserAccount[]) {
          console.table(user);
        }

      },
      error: (error: Error) => { console.log(error) },
      complete: () => { console.log('Data fetched!') }
    });

    this.destroyRef.onDestroy(() => {
      subscription.unsubscribe();
    });

  }


  public testAuthorization() {
    
    const subscription = this.loginService.testAuthorization().subscribe({
      next: (response) => { console.log(response); },
      error: (error) => { console.log(error); }
    });

    this.destroyRef.onDestroy(() => {subscription.unsubscribe();});
  }

}
