import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavbarComponent } from "./pageComponent/navbar/navbar.component";
import { ReactiveFormsModule } from '@angular/forms';
import { FooterComponent } from "./pageComponent/footer/footer.component";
import { PopupComponent } from './pageComponent/popup/popup.component';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, NavbarComponent, ReactiveFormsModule, FooterComponent,PopupComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {

  title = 'ProjetoIntegradorAngular';

  ngOnInit(): void {
    const saved = localStorage.getItem('theme');

    if (saved === 'dark') {
      document.documentElement.classList.add('dark-theme');
    }
  }

  toggleTheme() {
    const root = document.documentElement;

    if (root.classList.contains('dark-theme')) {
      root.classList.remove('dark-theme');
      localStorage.setItem('theme', 'light');
    } else {
      root.classList.add('dark-theme');
      localStorage.setItem('theme', 'dark');
    }
  }
}
