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
  isAdmin = false;
  UserRoles!: any[] | null
  authService = inject(AuthService);
  userService = inject(UserService);

  ngOnInit(): void {
    this.checkLogin();
    this.UserRoles = this.authService.getUserRoles()
  }

  toggleMenu() {
    this.isMenuOpen = !this.isMenuOpen;
  }

  closeMenu() {
    this.isMenuOpen = false;
  }
  chechkAdmin(){
    if(this.UserRoles?.some(r => r.includes("Admin"))){
      this.isAdmin = true
      console.log(this.UserRoles)
      return
    }
    this.isAdmin = false
  }
  checkLogin() {
    this.authService.isLoggedIn().subscribe(IsAuth => {
      this.isLogged = IsAuth;
      if (IsAuth) {
        const userId = this.authService.GetUserFromJwtToken().id;
        this.userService.GetUserById(userId).subscribe({
          next: res => {
            this.UserLogged = res
            this.chechkAdmin()

          }
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
