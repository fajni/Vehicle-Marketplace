import { Component } from '@angular/core';

@Component({
  selector: 'app-about',
  imports: [],
  templateUrl: './about.html',
  styleUrl: './about.css'
})
export class About {

  public members: TeamMember[] = [
    {
      firstname: "Jane",
      lastname: "Doe",
      title: "CEO & Founder",
      describe: "Some text that describes me lorem ipsum ipsum lorem.",
      email: "jane@example.com",
      imageSrc: "assets/about-us/3.jpg"
    },
    {
      firstname: "Mike",
      lastname: "Rose",
      title: "Art Director",
      describe: "Some text that describes me lorem ipsum ipsum lorem.",
      email: "mike@example.com",
      imageSrc: "assets/about-us/2.jpg"
    },
    {
      firstname: "John",
      lastname: "Doe",
      title: "Designer",
      describe: "Some text that describes me lorem ipsum ipsum lorem.",
      email: "john@example.com",
      imageSrc: "assets/about-us/1.jpg"
    },
    {
      firstname: "Veljko",
      lastname: "Fajnisevic",
      title: "Student",
      describe: "Master student looking for jobs and internships.",
      email: "veljko.fajnisevic.15@gmail.com",
      imageSrc: "assets/about-us/me.jpg"
    }
  ];

}

export interface TeamMember {
  
  firstname: string;
  lastname: string;
  title: string;
  describe: string;
  email: string;
  imageSrc: string;
}