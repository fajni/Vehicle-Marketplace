import { Component, DestroyRef, inject, OnInit, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Navbar } from "./navbar/navbar";
import { LoginService } from './login/login.service';
import { Footer } from "./footer/footer";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Navbar, Footer],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {

  public backendResponse: boolean = false;

  private loginService = inject(LoginService);
  private destroyRef = inject(DestroyRef);

  ngOnInit(): void {

    const subscription = this.loginService.getUsers().subscribe({
      next: () => {this.backendResponse = true; },
      error: () => { this.backendResponse = true; }
    });

    this.destroyRef.onDestroy(() => {subscription.unsubscribe()});

  }
  
}
