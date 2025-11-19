import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ViewUserPost } from '../../models/ViewUserPost';
import { UserPostService } from '../../Services/user-post.service';
import { CreateVoteRequest } from '../../models/CreateVoteRequest';
import { AuthService } from '../../Services/auth.service';
import { UserPost } from '../../models/UserPost';
import { CreateUserPost } from '../../models/CreateUserPost';
import { UserService } from '../../Services/user.service';

@Component({
  selector: 'app-feed',
  imports: [CommonModule, FormsModule],
  templateUrl: './feed.component.html',
  styleUrl: './feed.component.css'
})
export class FeedComponent implements OnInit {

  // --- Usuário logado (igual à Navbar) ---
  userService = inject(UserService);
  authService = inject(AuthService);
  UserLogged: any = null;

  // --- Posts ---
  postService = inject(UserPostService);
  posts: ViewUserPost[] = [];
  selectedPost: UserPost | null = null;

  // --- Modal de criação ---
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

  // ================================
  // Carregar usuário logado (igual navbar)
  // ================================
  loadLoggedUser() {
    this.authService.isLoggedIn().subscribe(isAuth => {
      if (!isAuth) {
        this.UserLogged = null;
        return;
      }

      const tokenUser = this.authService.GetUserFromJwtToken();
      if (!tokenUser?.id) return;

      this.userService.GetUserById(tokenUser.id).subscribe({
        next: res => {
          this.UserLogged = res;
        },
        error: err => console.error("Erro ao carregar usuário logado:", err)
      });
    });
  }

  // ================================
  // Carregar posts
  // ================================
  loadPosts(): void {
    this.postService.GetAll().subscribe({
      next: res => this.posts = res,
      error: err => console.error('Erro ao carregar posts:', err)
    });
  }

  // ================================
  // Modal do post
  // ================================
  openModal(id: string) {
    this.postService.GetById(id).subscribe({
      next: r => this.selectedPost = r,
      error: e => console.error('Erro ao carregar post:', e)
    });
  }

  closeModal() {
    this.selectedPost = null;
  }

  onOverlayClick(event: MouseEvent) {
    if ((event.target as HTMLElement).classList.contains('modal-overlay')) {
      this.closeModal();
    }
  }

  // ================================
  // VOTOS
  // ================================
  Vote(postId: string) {
    if (!this.UserLogged) return;

    const voteRequest: CreateVoteRequest = {
      postId,
      userId: this.UserLogged.id
    };

    this.postService.Vote(voteRequest).subscribe({
      next: () => {
        this.loadPosts();
        // Atualiza post dentro do modal
        if (this.selectedPost?.id === postId) {
          this.postService.GetById(postId).subscribe(r => this.selectedPost = r);
        }
      },
      error: () => this.loadPosts()
    });
  }

  // ================================
  // MODAL DE CRIAÇÃO
  // ================================
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
    this.createImageFile = null;
    this.createImagePreview = null;
    this.createError = null;
  }

  onCreateFileChange(event: Event) {
    const input = event.target as HTMLInputElement;

    if (!input.files?.length) {
      this.createImageFile = null;
      this.createImagePreview = null;
      return;
    }

    const file = input.files[0];
    this.createImageFile = file;

    const reader = new FileReader();
    reader.onload = () => this.createImagePreview = reader.result as string;
    reader.readAsDataURL(file);
  }

  // ================================
  // Criar post
  // ================================
submitCreate() {
  this.createError = null;

  if (!this.createTitle.trim() || !this.createDescription.trim()) {
    this.createError = 'Título e descrição são obrigatórios.';
    return;
  }

  if (!this.UserLogged) {
    this.createError = 'Você precisa estar logado para publicar.';
    return;
  }

  this.creating = true;

  const fd = new FormData();
  fd.append('Title', this.createTitle);
  fd.append('Description', this.createDescription);
  fd.append('UserId', this.UserLogged.id);

  // Só adiciona o arquivo se existir
  if (this.createImageFile) {
    fd.append('PostImg', this.createImageFile);
  }

  this.postService.Post(fd).subscribe({
    next: () => {
      this.creating = false;
      this.closeCreateModal();
      this.loadPosts();
    },
    error: err => {
      console.error(err);
      this.creating = false;
      this.createError = 'Erro ao criar post.';
    }
  });
}
  // Finalizar criação
  private finishCreation() {
    this.creating = false;
    this.closeCreateModal();
    this.loadPosts();
  }
}
