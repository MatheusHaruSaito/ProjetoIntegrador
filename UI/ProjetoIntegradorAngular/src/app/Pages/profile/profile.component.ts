import { Component, inject, OnInit } from '@angular/core';
import { RouterModule, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { UserProfile } from '../../models/UserProfile';
import { AuthService } from '../../Services/auth.service';
import { UserService } from '../../Services/user.service';
import { UserPostService } from '../../Services/user-post.service';
import { ViewUserPost } from '../../models/ViewUserPost';
import { UserPost } from '../../models/UserPost';
import { CreateVoteRequest } from '../../models/CreateVoteRequest';

@Component({
  selector: 'app-profile',
  imports: [RouterModule, CommonModule],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent implements OnInit {

  User!: UserProfile;
  UserPosts?: ViewUserPost[];

  selectedPost: UserPost | null = null;
  loggedUser: any = null;

  // injeção via inject (segura em qualquer ordem se não usar this na declaração)
  authService = inject(AuthService);
  userService = inject(UserService);
  postService = inject(UserPostService);
  router = inject(Router);

  ngOnInit(): void {
    // pegamos o usuário somente depois da injeção
    this.loggedUser = this.authService.GetUserFromJwtToken();

    if (!this.loggedUser?.id) {
      // se não estiver logado, redireciona
      this.router.navigate(['/Login']);
      return;
    }

    // busca dados do perfil e posts do usuário
    this.userService.GetProfileInfo(this.loggedUser.id).subscribe({
      next: (res) => {
        this.User = res;
        this.UserPosts = res.userPosts ?? [];

        const profileImg = document.getElementById('ProfileImage') as HTMLImageElement | null;
        if (profileImg && res.profileImgPath) profileImg.src = res.profileImgPath;
      },
      error: (err) => {
        console.error('Erro ao carregar perfil:', err);
      }
    });

    // aplica tema salvo
    const savedTheme = localStorage.getItem('theme');
    if (savedTheme === 'dark') document.documentElement.classList.add('dark-theme');
  }

  // --- Modal ---
  openModal(postId: string): void {
    // chama API para obter o post completo (comentários, votos, etc.)
    this.postService.GetById(postId).subscribe({
      next: (post) => {
        this.selectedPost = post;
        // opcional: evitar que o scroll da página continue (se quiser)
        // document.body.style.overflow = 'hidden';
      },
      error: (err) => {
        console.error('Erro ao abrir post:', err);
      }
    });
  }

  closeModal(): void {
    this.selectedPost = null;
    // document.body.style.overflow = '';
  }

  onOverlayClick(event: MouseEvent): void {
    if ((event.target as HTMLElement).classList.contains('modal-overlay')) {
      this.closeModal();
    }
  }

  // --- Votos ---
  Vote(postId: string): void {
    if (!this.loggedUser?.id) {
      alert('Você precisa estar logado para votar.');
      return;
    }

    const req: CreateVoteRequest = {
      postId,
      userId: this.loggedUser.id
    };

    this.postService.Vote(req).subscribe({
      next: () => {
        // ideal: atualizar apenas o post/votes localmente — aqui usamos reload simples
        window.location.reload();
      },
      error: (err) => {
        console.error('Erro ao votar:', err);
        window.location.reload();
      }
    });
  }

  // --- Theme toggle (resolve o erro NG9) ---
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

  // --- Logout simples (botão lateral) ---
  logout(): void {
    this.authService.Logout();
    // redireciona para login
    this.router.navigate(['/Login']);
  }
}
