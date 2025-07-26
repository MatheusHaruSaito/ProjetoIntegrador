import { Component, inject } from '@angular/core';
import { RouterModule } from '@angular/router';
import { UserProfile } from '../../models/UserProfile';
import { AuthService } from '../../Services/auth.service';
import { UserService } from '../../Services/user.service';

@Component({
  selector: 'app-profile',
  imports: [RouterModule],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent {
  User! : UserProfile

  authService = inject(AuthService);
  userService = inject(UserService);
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
    var LoggedUser = this.authService.GetUserFromJwtToken()
    this.userService.GetProfileInfo(LoggedUser.email).subscribe({
      next: res=>{
        this.User = res
      },
      error: err=>{
        console.log(err);
      }
    })
    const savedTheme = localStorage.getItem('theme');
    if (savedTheme === 'dark') {
      document.documentElement.classList.add('dark-theme');
    }
  }
  
}
