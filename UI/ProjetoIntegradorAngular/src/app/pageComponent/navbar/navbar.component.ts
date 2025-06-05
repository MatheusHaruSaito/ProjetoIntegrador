import { CommonModule } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AuthService } from '../../Services/auth.service';

@Component({
  selector: 'app-navbar',
  imports: [RouterModule, CommonModule],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})

export class NavbarComponent implements OnInit {

  constructor(private authService: AuthService) {
    
  }
  ngOnInit(): void {
    this.checkLogin();
  }
  isMenuOpen = false;
  isLogged:boolean = false;
  toggleMenu() {
    this.isMenuOpen = !this.isMenuOpen;
  }
  checkLogin(){
    this.isLogged = this.authService.isLoggedIn();
  }
  logout(){
    this.authService.Logout();
    window.alert("Usuario Deslogado");
    window.location.reload();
  }
  
}
