import { Component, DestroyRef, inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { UserAccount } from '../models/UserAccount';
import { RegistrationService } from './registration.service';

@Component({
  selector: 'app-registration',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './registration.html',
  styleUrl: './registration.css'
})
export class Registration {

  get invalidEmail() {
    return (
      this.registrationForm.controls.email.touched &&
      this.registrationForm.controls.email.dirty &&
      this.registrationForm.controls.email.invalid
    );
  }

  get invalidPassword() {
    return (
      this.registrationForm.controls.password.touched &&
      this.registrationForm.controls.password.dirty &&
      this.registrationForm.controls.password.invalid
    );
  }

  private registrationService = inject(RegistrationService);
  private destroyRef = inject(DestroyRef);

  public nullValues: boolean = false;
  public successfulRegistration: boolean | undefined;
  public message: string = "";

  public roles = [
    { value: 'Admin', label: 'Admin' },
    { value: 'User', label: 'User' }
  ];

  public registrationForm = new FormGroup({
    firstname: new FormControl<string>('', { validators: [Validators.required] }),
    lastname: new FormControl<string>('', { validators: [Validators.required] }),
    email: new FormControl<string>('', { validators: [ Validators.email, Validators.required ] }),
    password: new FormControl<string>('', { validators: [ Validators.minLength(5), Validators.required ] }),
    role: new FormControl<string>('', { validators: [Validators.required] })
  });

  public onSubmit() {

    if(this.registrationForm.valid){

      var userAccount: UserAccount = new UserAccount(
        0,
        this.registrationForm.value.firstname!,
        this.registrationForm.value.lastname!,
        this.registrationForm.value.email!,
        this.registrationForm.value.password!,
        this.registrationForm.value.role!
      );

      console.log(userAccount.format());

      const subscribe = this.registrationService.registerUser(userAccount).subscribe({
        next: (response) => { console.log(response.message); this.message = response.message },
        error: (error: Error) => { console.log(error) }
      });

      this.destroyRef.onDestroy(() => { subscribe.unsubscribe(); })

      this.successfulRegistration = true;
    }
    else {
      this.nullValues = true;
      this.successfulRegistration = false;
    }
  }
}