import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { ViewUserPost } from '../../models/ViewUserPost';
import { UserPostService } from '../../Services/user-post.service';
import { CreateVoteRequest } from '../../models/CreateVoteRequest';
import { AuthService } from '../../Services/auth.service';
import { ViewUser } from '../../models/ViewUser';
import { UserPost } from '../../models/UserPost';

@Component({
  selector: 'app-feed',
  imports: [CommonModule],
  templateUrl: './feed.component.html',
  styleUrl: './feed.component.css'
})
export class FeedComponent implements OnInit {
  posts: ViewUserPost[] = [];
  loggedUser?: ViewUser;
  selectedPost: UserPost | null = null; 


  postService = inject(UserPostService);
  authService = inject(AuthService);

  ngOnInit(): void {
    this.postService.GetAll().subscribe(response => {
      this.posts = response;
      console.log(this.posts);
    });

    this.loggedUser = this.authService.GetUserFromJwtToken();
  }

  openModal(id: string) {
    this.postService.GetById(id).subscribe({
      next: r =>{
        this.selectedPost = r
      }
    }); 
    console.log(this.selectedPost)
  }

  closeModal() {
    this.selectedPost = null;
  }

  onOverlayClick(event: MouseEvent) {
    if ((event.target as HTMLElement).classList.contains('modal-overlay')) {
      this.closeModal();
    }
  }

  Vote(postId: string) {
    console.log("clicado");
    if (this.loggedUser == null) {
      console.log("usuario não logado");
      return;
    }

    const voteRequest: CreateVoteRequest = {
      postId,
      userId: this.loggedUser!.id
    };

    this.postService.Vote(voteRequest).subscribe({
      next: res => {
        window.location.reload();
        console.log(res);
      },
      error: res => {
        window.location.reload();
        console.log(res);
      }
    });
  }
}
