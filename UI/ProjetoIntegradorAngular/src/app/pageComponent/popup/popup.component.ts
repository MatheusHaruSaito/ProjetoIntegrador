import { Component, inject } from '@angular/core';
import { PopupService } from '../../Services/popup.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-popup',
  standalone: true,
  imports: [CommonModule], 
  templateUrl: './popup.component.html',
  styleUrl: './popup.component.css'
})
export class PopupComponent {

  popup = inject(PopupService);

  get message() {
    return this.popup.message();
  }

  get closing() {
    return this.popup.closing();
  }

  onAnimationEnd() {
    if (this.closing) {
      this.popup.message.set(null);
      this.popup.closing.set(false);
    }
  }
}
