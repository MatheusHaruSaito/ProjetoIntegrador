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

  authService = inject(AuthService);
  userService = inject(UserService);
  postService = inject(UserPostService);
  router = inject(Router);

  ngOnInit(): void {
    this.loggedUser = this.authService.GetUserFromJwtToken();

    if (!this.loggedUser?.id) {
      this.router.navigate(['/Login']);
      return;
    }

    this.userService.GetProfileInfo(this.loggedUser.id).subscribe({
      next: (res) => {
        this.User = res;
        this.UserPosts = res.userPosts ?? [];

        const profileImg = document.getElementById('ProfileImage') as HTMLImageElement | null;
        if (profileImg && res.profileImgPath) {
          profileImg.src = res.profileImgPath;
        }
      },
      error: (err) => console.error('Erro ao carregar perfil:', err)
    });
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

  openModal(postId: string): void {
    this.postService.GetById(postId).subscribe({
      next: (post) => this.selectedPost = post,
      error: (err) => console.error('Erro ao abrir post:', err)
    });
  }

  closeModal(): void {
    this.selectedPost = null;
  }

  onOverlayClick(event: MouseEvent): void {
    if ((event.target as HTMLElement).classList.contains('modal-overlay')) {
      this.closeModal();
    }
  }

  Vote(postId: string): void {
    if (!this.loggedUser?.id) return;

    const req: CreateVoteRequest = {
      postId,
      userId: this.loggedUser.id
    };

    this.postService.Vote(req).subscribe({
      next: () => window.location.reload(),
      error: () => window.location.reload()
    });
  }

  logout(): void {
    this.authService.Logout();
    this.router.navigate(['/Login']);
  }
}
