import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

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
export class Navbar{

  public userLoggedIn: boolean = false;

}

