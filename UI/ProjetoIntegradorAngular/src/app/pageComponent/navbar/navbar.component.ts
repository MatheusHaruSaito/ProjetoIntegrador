import { CommonModule } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AuthService } from '../../Services/auth.service';
import { ViewUser } from '../../models/ViewUser';
@Component({
  selector: 'app-navbar',
  imports: [RouterModule, CommonModule],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})

export class NavbarComponent implements OnInit {
   User?: ViewUser;
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
    
    this.authService.isLoggedIn().subscribe(IsAuth=>{
      this.isLogged = IsAuth
      if(IsAuth){
      this.User = this.authService.GetUserFromJwtToken()
    }
    });

  }
  logout(){
    this.authService.Logout();
    window.alert("Usuario Deslogado");
    window.location.reload();
  }
  
}
