import { Component, inject, OnInit } from '@angular/core';
import { CreateVoteRequest } from '../../models/CreateVoteRequest';
import { UserProfile } from '../../models/UserProfile';
import { UserPostService } from '../../Services/user-post.service';
import { UserService } from '../../Services/user.service';
import { AuthService } from '../../Services/auth.service';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { PopupService } from '../../Services/popup.service';

@Component({
  selector: 'app-profilemenu',
  imports: [RouterModule, CommonModule],
  templateUrl: './profilemenu.component.html',
  styleUrl: './profilemenu.component.css'
})
export class ProfilemenuComponent implements OnInit {

  User!: UserProfile;
  loggedUser: any = null;

  authService = inject(AuthService);
  userService = inject(UserService);
  postService = inject(UserPostService);
  router = inject(Router);
  popup = inject(PopupService);

  ngOnInit(): void {
    this.loggedUser = this.authService.GetUserFromJwtToken();

    if (!this.loggedUser?.id) {
      this.router.navigate(['/Login']);
      return;
    }
  }

  toggleTheme(): void {
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

  logout(): void {
    this.authService.Logout();
    this.popup.show("Você saiu da sua conta."); 
    this.router.navigate(['/Login']);
  }

}
