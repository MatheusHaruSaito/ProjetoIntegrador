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

@Component({
  selector: 'app-feed',
  imports: [CommonModule, FormsModule],
  templateUrl: './feed.component.html',
  styleUrl: './feed.component.css'
})
export class FeedComponent implements OnInit {

  // ==== Usuário logado ====
  userService = inject(UserService);
  authService = inject(AuthService);
  popup = inject(PopupService);
  UserLogged: any = null;

  // ==== Posts ====
  postService = inject(UserPostService);
  posts: ViewUserPost[] = [];
  selectedPost: UserPost | null = null;

  // ==== Comentários ====
  currentComments: PostComment[] = [];
  newCommentText = '';

  // ==== Modal de criação ====
  createModalOpen = false;
  createTitle = '';
  createDescription = '';
  createImageFile: File | null = null;
  createImagePreview: string | null = null;
  creating = false;
  createError: string | null = null;

  ngOnInit(): void {
    this.loadPosts();
    this.loadLoggedUser();
  }

  loadLoggedUser() {
    this.authService.isLoggedIn().subscribe(isAuth => {
      if (!isAuth) {
        this.UserLogged = null;
        return;
      }

      const tokenUser = this.authService.GetUserFromJwtToken();
      if (!tokenUser?.id) return;

      this.userService.GetUserById(tokenUser.id).subscribe({
        next: user => this.UserLogged = user
      });
    });
  }

  loadPosts() {
    this.postService.GetAll().subscribe({
      next: res => this.posts = res
    });
  }

  openModal(id: string) {
    this.postService.GetById(id).subscribe({
      next: r => {
        this.selectedPost = r;
        this.currentComments = [...(r.comments || [])];
        this.newCommentText = '';
      }
    });
  }

  closeModal() {
    this.selectedPost = null;
    this.currentComments = [];
    this.newCommentText = '';
  }

  onOverlayClick(event: MouseEvent) {
    if ((event.target as HTMLElement).classList.contains('modal-overlay')) {
      this.closeModal();
    }
  }

  Vote(postId: string) {
    if (!this.UserLogged) return;

    const req: CreateVoteRequest = {
      postId,
      userId: this.UserLogged.id
    };

    this.postService.Vote(req).subscribe({
      next: () => {
        this.loadPosts();
        if (this.selectedPost?.id === postId) {
          this.postService.GetById(postId).subscribe(p => this.selectedPost = p);
        }
      }
    });
  }

  sendComment() {
    if (!this.UserLogged || !this.selectedPost || !this.newCommentText.trim()) return;

    const req: CreateComment = {
      postId: this.selectedPost.id,
      userId: this.UserLogged.id,
      text: this.newCommentText.trim()
    };

    this.postService.Comment(req).subscribe({
      next: () => {
        this.newCommentText = '';
        this.reloadComments();
      }
    });
  }

  reloadComments() {
    if (!this.selectedPost) return;

    this.postService.GetById(this.selectedPost.id).subscribe({
      next: p => {
        this.currentComments = [...(p.comments || [])];
        this.selectedPost = p;
      }
    });
  }

  voteComment(commentId: string) {
    if (!this.UserLogged) return;

    const req: CreateCommentVoteRequest = {
      userId: this.UserLogged.id,
      commentId
    };

    this.postService.CommentVote(req).subscribe({
      next: () => this.reloadComments()
    });
  }


  openCreateModal() {
    this.createModalOpen = true;
    this.createTitle = '';
    this.createDescription = '';
    this.createImageFile = null;
    this.createImagePreview = null;
    this.createError = null;
  }

  closeCreateModal() {
    this.createModalOpen = false;
  }

  onCreateFileChange(event: Event) {
    const input = event.target as HTMLInputElement;

    if (!input.files?.length) {
      this.createImageFile = null;
      this.createImagePreview = null;
      return;
    }

    this.createImageFile = input.files[0];

    const reader = new FileReader();
    reader.onload = () => this.createImagePreview = reader.result as string;
    reader.readAsDataURL(this.createImageFile);
  }

  submitCreate() {
    this.createError = null;

    if (!this.createTitle.trim() || !this.createDescription.trim()) {
      this.createError = 'Título e descrição são obrigatórios.';
      return;
    }

    if (!this.UserLogged) {
      this.createError = 'Você precisa estar logado.';
      return;
    }

    this.creating = true;

    const fd = new FormData();
    fd.append('Title', this.createTitle);
    fd.append('Description', this.createDescription);
    fd.append('UserId', this.UserLogged.id);

    if (this.createImageFile) {
      fd.append('PostImg', this.createImageFile);
    }

    this.postService.Post(fd).subscribe({
      next: () => {
        this.creating = false;
        this.closeCreateModal();
        this.loadPosts();
      },
      error: () => {
        this.creating = false;
        this.createError = 'Erro ao criar post.';
      }
    });
  }
}
