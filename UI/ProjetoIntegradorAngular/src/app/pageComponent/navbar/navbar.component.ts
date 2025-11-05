import { CommonModule } from '@angular/common';
import { Component, inject, Inject, OnInit } from '@angular/core';
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
  constructor(private authService: AuthService) {
    
  }

  userService = inject(UserService)
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
      var userId = this.authService.GetUserFromJwtToken().id
      this.userService.GetUserById(userId).subscribe({
        next: response =>{
          this.UserLogged = response
        }
      })
    }
  });

  }
  logout(){
    this.authService.Logout();
    window.alert("Usuario Deslogado");
    window.location.reload();
  }
  
}
