import { Component, Input, model, OnInit } from '@angular/core';

@Component({
  selector: 'app-popup-message',
  imports: [],
  templateUrl: './popup-message.html',
  styleUrl: './popup-message.css'
})
export class PopupMessage implements OnInit{

  @Input({required: true}) showHtml!: boolean;

  @Input({required: true}) message!: string;
  @Input({required: true}) title!: string;

  public ngOnInit(): void {
    alert(this.title + " \n" + this.message);
  }

}
