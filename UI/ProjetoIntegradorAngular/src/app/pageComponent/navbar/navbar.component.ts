import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AuthService } from '../../Services/auth.service';
import { UserService } from '../../Services/user.service';
import { User } from '../../models/user';

@Component({
  selector: 'app-navbar',
  imports: [RouterModule, CommonModule],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent implements OnInit {
  UserLogged?: User;
  isMenuOpen = false;
  isLogged = false;

  authService = inject(AuthService);
  userService = inject(UserService);

  ngOnInit(): void {
    this.checkLogin();
  }

  toggleMenu() {
    this.isMenuOpen = !this.isMenuOpen;
  }

  closeMenu() {
    this.isMenuOpen = false;
  }

  checkLogin() {
    this.authService.isLoggedIn().subscribe(IsAuth => {
      this.isLogged = IsAuth;
      if (IsAuth) {
        const userId = this.authService.GetUserFromJwtToken().id;
        this.userService.GetUserById(userId).subscribe({
          next: res => (this.UserLogged = res)
        });
      }
    });
  }

  logout() {
    this.authService.Logout();
    alert('Usuário deslogado');
    window.location.reload();
  }
}
