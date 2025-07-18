import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-home',
  imports: [RouterModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {

  toggleTheme() {
    const root = document.documentElement;
    const isDark = root.classList.contains('dark-theme');
  
    if (isDark) {
      root.classList.remove('dark-theme');
      localStorage.setItem('theme', 'light');
    } else {
      root.classList.add('dark-theme');
      localStorage.setItem('theme', 'dark');
    }
  }

  
  ngOnInit() {
    const savedTheme = localStorage.getItem('theme');
    if (savedTheme === 'dark') {
      document.documentElement.classList.add('dark-theme');
    }
  }

}
