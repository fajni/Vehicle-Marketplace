import { Component, DestroyRef, inject, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { LoginService } from '../login/login.service';
import { MyAccountService } from './my-account.service';
import { UserAccount } from '../models/UserAccount';
import { filter, switchMap, tap } from 'rxjs';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-my-account',
  imports: [RouterLink, ReactiveFormsModule],
  templateUrl: './my-account.html',
  styleUrl: './my-account.css'
})
export class MyAccount implements OnInit{

  public authorized: boolean = false;
  public message: string = "";
  public roles = [
    { value: 'Admin', label: 'Admin' },
    { value: 'User', label: 'User' }
  ];

  public user?: UserAccount | undefined;

  public updateForm = new FormGroup({
    id: new FormControl<number>(0, { }),
    firstname: new FormControl<string>('', { validators: [Validators.required] }),
    lastname: new FormControl<string>('', { validators: [Validators.required] }),
    email: new FormControl<string>('', { validators: [ Validators.email, Validators.required ] }),
    password: new FormControl<string>('', { validators: [ Validators.minLength(5), Validators.required ] }),
    role: new FormControl<string>('', { validators: [Validators.required] })
  });

  private loginService = inject(LoginService);
  private myAccountService = inject(MyAccountService);
  private destroyRef = inject(DestroyRef);

  public ngOnInit() {

    const subscription = this.loginService.testAuthorization().pipe(

      filter(response => response.loggedIn && !!response.userEmail), // only valid ones are allowed
      tap(() => this.authorized = true), // set the authorized
      switchMap(response => this.myAccountService.getUserByEmail(response.userEmail!)) // returns new Observable<UserAccount>
      
    ).subscribe({

      next: (user) => {
        this.user = user;
        this.updateForm.get('id')?.setValue(user?.id!);
        this.updateForm.get('firstname')?.setValue(user?.firstname!);
        this.updateForm.get('lastname')?.setValue(user?.lastname!);
        this.updateForm.get('email')?.setValue(user?.email!);
        this.updateForm.get('password')?.setValue(user?.password!);
        this.updateForm.get('role')?.setValue(user?.role!);
      },
      error: (err) => {
        this.message = err;
        console.log(err);
      }

    });

    this.destroyRef.onDestroy(() => subscription.unsubscribe());

  }

  public updateUserAccount() {

    var updatedUserAccount: UserAccount = new UserAccount(
        this.updateForm.value.id!,
        this.updateForm.value.firstname!,
        this.updateForm.value.lastname!,
        this.updateForm.value.email!,
        this.updateForm.value.password!,
        this.updateForm.value.role!
    );

    console.log(updatedUserAccount);
    const result = confirm("Update the account?");

    if(result) {
      const subscription = this.myAccountService.updateUserAccount(this.user?.id!, updatedUserAccount).subscribe({
        next: (response) => { console.log(response); this.message = `Successfully updated user with ${this.user?.id} id!`; },
        error: (error) => { console.log(error) }
      });

      this.destroyRef.onDestroy(() => { subscription.unsubscribe(); });
    }

  }

  public deleteUserAccount(id: number) {

    const result = confirm("Delete the account?");

    if(result) {

      const subscriptionDelete = this.myAccountService.deleteUserAccount(id).subscribe({
        next: (response) => { console.log(response); this.message = `Successfully deleted user with ${this.user?.id} id!`; },
        error: (error) => { console.log(error); }
      });

      const subscriptionLogout = this.loginService.logout().subscribe({
        next: (response) => { console.log(response); },
        error: (error) => { console.log(error); }
      })

      this.destroyRef.onDestroy(() => { 
        subscriptionDelete.unsubscribe(); 
        subscriptionLogout.unsubscribe();
      });

      window.location.reload();

    }

  }

}
