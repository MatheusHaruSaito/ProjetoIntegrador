import { Component } from '@angular/core';

@Component({
  selector: 'app-feedback',
  imports: [],
  templateUrl: './feedback.component.html',
  styleUrl: './feedback.component.css'
})
export class FeedbackComponent {

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
