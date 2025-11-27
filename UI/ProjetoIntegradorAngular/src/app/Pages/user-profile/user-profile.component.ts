import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { UserService } from '../../Services/user.service';
import { UserPostService } from '../../Services/user-post.service';
import { PopupService } from '../../Services/popup.service';
import { AuthService } from '../../Services/auth.service';
import { CreateComment } from '../../models/CreateComment';
import { CreateCommentVoteRequest } from '../../models/CreateCommentVoteRequest';
import { CreateVoteRequest } from '../../models/CreateVoteRequest';
import { ViewUserPost } from '../../models/ViewUserPost';
import { UserPost } from '../../models/UserPost';
import { PostComment } from '../../models/PostComment';

@Component({
  selector: 'app-user-profile',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './user-profile.component.html',
  styleUrl: './user-profile.component.css'
})
export class UserProfileComponent implements OnInit {

  route = inject(ActivatedRoute);
  userService = inject(UserService);
  postService = inject(UserPostService);
  popupService = inject(PopupService);
  authService = inject(AuthService);
  router = inject(Router);

  loggedUser: any = null;
  user: any = null;

  userPosts: ViewUserPost[] = [];

  selectedPost: UserPost | null = null;
  currentComments: PostComment[] = [];
  newCommentText: string = '';

  ngOnInit() {
    this.loadLoggedUser();
    this.loadUserProfile();
  }

  loadLoggedUser() {
    this.loggedUser = this.authService.GetUserFromJwtToken();
  }

  loadUserProfile() {
    const id = this.route.snapshot.paramMap.get('id');
    if (!id) return;

    this.userService.GetUserById(id).subscribe({
      next: u => this.user = u
    });

    this.postService.GetAll().subscribe({
      next: posts => {
        this.userPosts = posts.filter(p => p.userId === id);
      }
    });
  }

  openPostModal(postId: string): void {
    console.log('Abrindo modal para post ID:', postId);
    this.postService.GetById(postId).subscribe({
      next: (post) => {
        console.log('Post carregado:', post);
        this.selectedPost = post;
        this.currentComments = [...(post.comments || [])];
        this.newCommentText = '';
      },
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
    if (!this.loggedUser?.id) {
      this.popupService.show("Faça login para votar");
      return;
    }

    const req: CreateVoteRequest = {
      postId,
      userId: this.loggedUser.id
    };

    this.postService.Vote(req).subscribe({
      next: () => {
        this.userPosts = this.userPosts.map(p =>
          p.id === postId ? { ...p, votes: p.votes + 1 } : p
        );

        if (this.selectedPost?.id === postId) {
          this.postService.GetById(postId).subscribe(updated => {
            this.selectedPost = updated;
          });
        }
      },
      error: (err) => {
        console.error('Erro ao votar:', err);
      }
    });
  }

  reloadComments() {
    if (!this.selectedPost) return;

    this.postService.GetById(this.selectedPost.id).subscribe({
      next: (post) => {
        this.currentComments = [...(post.comments || [])];
        this.selectedPost = post;
      }
    });
  }

  sendComment() {
    if (!this.loggedUser) {
      this.popupService.show("Faça login para comentar");
      return;
    }

    if (!this.selectedPost || !this.newCommentText.trim()) {
      this.popupService.show("Digite um comentário");
      return;
    }

    const req: CreateComment = {
      postId: this.selectedPost.id,
      userId: this.loggedUser.id,
      text: this.newCommentText.trim()
    };

    this.postService.Comment(req).subscribe({
      next: () => {
        this.newCommentText = '';
        this.reloadComments();
        this.popupService.show("Comentário publicado!");
      },
      error: (err) => {
        console.error('Erro ao comentar:', err);
        this.popupService.show("Erro ao publicar comentário");
      }
    });
  }

  voteComment(commentId: string) {
    if (!this.loggedUser?.id) {
      this.popupService.show("Faça login para votar");
      return;
    }

    const req: CreateCommentVoteRequest = {
      userId: this.loggedUser.id,
      commentId
    };

    this.postService.CommentVote(req).subscribe({
      next: () => {
        this.reloadComments();
      },
      error: (err) => {
        console.error('Erro ao votar no comentário:', err);
      }
    });
  }

  navigateToUserProfile(userId: string, event: Event): void {
    event.stopPropagation();
    if (userId) {
      this.router.navigate(['/UserProfile', userId]);
    }
  }
}