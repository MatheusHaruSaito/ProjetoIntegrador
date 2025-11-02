import { Component, inject, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { UserProfile } from '../../models/UserProfile';
import { AuthService } from '../../Services/auth.service';
import { UserService } from '../../Services/user.service';
import { NgIf } from "../../../../node_modules/@angular/common/index";
import { ViewUserPost } from '../../models/ViewUserPost';

@Component({
  selector: 'app-profile',
  imports: [RouterModule],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent implements OnInit {

  User! : UserProfile
  UserPosts?: ViewUserPost[]
  authService = inject(AuthService);
  userService = inject(UserService);

  ngOnInit(): void {
       var LoggedUser = this.authService.GetUserFromJwtToken()
       console.log(LoggedUser)
    this.userService.GetProfileInfo(LoggedUser.id).subscribe({
      next: res=>{
        this.User = res,
        this.UserPosts = this.User.userPosts
        console.log(res),
        (document.getElementById("ProfileImage") as HTMLImageElement).src = res.profileImgPath
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
}
