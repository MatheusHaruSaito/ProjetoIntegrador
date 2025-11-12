import { Component, inject, OnInit } from '@angular/core';
import { RouterModule, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { UserProfile } from '../../models/UserProfile';
import { AuthService } from '../../Services/auth.service';
import { UserService } from '../../Services/user.service';
import { ViewUserPost } from '../../models/ViewUserPost';

@Component({
  selector: 'app-profile',
  imports: [RouterModule, CommonModule],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent implements OnInit {

  User!: UserProfile;
  UserPosts?: ViewUserPost[];

  // Injeções de dependências
  authService = inject(AuthService);
  userService = inject(UserService);
  router = inject(Router);

  ngOnInit(): void {
    const LoggedUser = this.authService.GetUserFromJwtToken();

    if (!LoggedUser || !LoggedUser.id) {
      // Se não houver usuário logado, redireciona para login
      this.router.navigate(['/Login']);
      return;
    }

    // Busca informações do perfil
    this.userService.GetProfileInfo(LoggedUser.id).subscribe({
      next: (res) => {
        this.User = res;
        this.UserPosts = res.userPosts;

        const profileImg = document.getElementById("ProfileImage") as HTMLImageElement;
        if (profileImg && res.profileImgPath) {
          profileImg.src = res.profileImgPath;
        }
      },
      error: (err) => {
        console.error('Erro ao carregar perfil:', err);
      }
    });

    // Aplica o tema salvo
    const savedTheme = localStorage.getItem('theme');
    if (savedTheme === 'dark') {
      document.documentElement.classList.add('dark-theme');
    }
  }

  // Alterna entre modo claro/escuro
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
    window.alert('Usuário deslogado com sucesso!');
    this.router.navigate(['/Login']);
  }
}
