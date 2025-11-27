import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ViewUserPost } from '../../models/ViewUserPost';
import { UserPostService } from '../../Services/user-post.service';
import { CreateVoteRequest } from '../../models/CreateVoteRequest';
import { AuthService } from '../../Services/auth.service';
import { UserPost } from '../../models/UserPost';
import { UserService } from '../../Services/user.service';
import { PostComment } from '../../models/PostComment';
import { CreateComment } from '../../models/CreateComment';
import { CreateCommentVoteRequest } from '../../models/CreateCommentVoteRequest';
import { PopupService } from '../../Services/popup.service';
import { SearchService } from '../../Services/search.service';
import { SearchTypeEnum } from '../../models/SearchTypeEnum';
import { Router, RouterModule } from '@angular/router';
import { ProfilemenuComponent } from '../../pageComponent/profilemenu/profilemenu.component';
import { UserProfile } from '../../models/UserProfile';

@Component({
  selector: 'app-profile',
  imports: [RouterModule, CommonModule, ProfilemenuComponent, FormsModule],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent implements OnInit {

  User!: UserProfile;
  UserPosts?: ViewUserPost[];

  selectedPost: UserPost | null = null;
  loggedUser: any = null;

  currentComments: PostComment[] = [];
  newCommentText = '';

  authService = inject(AuthService);
  userService = inject(UserService);
  postService = inject(UserPostService);
  router = inject(Router);
  popupService = inject(PopupService);

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

  openModal(postId: string): void {
    this.postService.GetById(postId).subscribe({
      next: (post) => {
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
    if (!this.loggedUser?.id) return;
    const req: CreateVoteRequest = {
      postId,
      userId: this.loggedUser.id
    };
    this.postService.Vote(req).subscribe({
      next: () => {
        if (this.UserPosts) {
          this.UserPosts = this.UserPosts.map(p =>
            p.id === postId ? { ...p, votes: p.votes + 1 } : p
          );
        }
        if (this.selectedPost?.id === postId) {
          this.postService.GetById(postId).subscribe(updated => {
            this.selectedPost = updated;
          });
        }
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
    if (!this.loggedUser || !this.selectedPost || !this.newCommentText.trim()) return;

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
      error: () => {
        this.popupService.show("Erro ao publicar comentário");
      }
    });
  }

  voteComment(commentId: string) {
    if (!this.loggedUser) return;

    const req: CreateCommentVoteRequest = {
      userId: this.loggedUser.id,
      commentId
    };

    this.postService.CommentVote(req).subscribe({
      next: () => this.reloadComments()
    });
  }

}
