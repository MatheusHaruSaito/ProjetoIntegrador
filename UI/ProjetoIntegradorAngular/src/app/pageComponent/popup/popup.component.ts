import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-popup',
  imports: [CommonModule],
  templateUrl: './popup.component.html',
  styleUrl: './popup.component.css'
}) 
export class PopupComponent implements OnInit{
  closing  = false;
  @Input() message: string = ''
  @Output() close = new EventEmitter<void>();

  ngOnInit() {
    setTimeout(() => this.closePopup(),4500);
  }

  closePopup(){
    this.closing = true;
  }

  onAnimationEnd(){
    if(this.closing ){
    this.close.emit()
    }
  }
}
