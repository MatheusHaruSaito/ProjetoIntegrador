import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { ViewUserPost } from '../../models/ViewUserPost';
import { UserPostService } from '../../Services/user-post.service';
import { response } from 'express';
import { CreateVoteRequest } from '../../models/CreateVoteRequest';
import { AuthService } from '../../Services/auth.service';
import { ViewUser } from '../../models/ViewUser';

@Component({
  selector: 'app-feed',
  imports: [CommonModule],
  templateUrl: './feed.component.html',
  styleUrl: './feed.component.css'
})
export class FeedComponent implements OnInit {
    posts: ViewUserPost[]= []
    loggedUser?: ViewUser
    postService = inject(UserPostService)
    authService = inject(AuthService)
  ngOnInit(): void {
    this.postService.GetAll().subscribe(response =>{
      this.posts = response;
      console.log(this.posts)
    })
    this.loggedUser = this.authService.GetUserFromJwtToken()
  }
    Vote(postId: string){
      console.log("clicado")
      if(this.loggedUser == null){
        console.log("usuario não logado")
        return
      }
      var voteRequest: CreateVoteRequest ={postId,userId: this.loggedUser!.id}
      this.postService.Vote(voteRequest).subscribe({
        next: res =>{
          window.location.reload()
          console.log(res)
        },
        error: res =>{
          window.location.reload()
          console.log(res)
        }
      })
    }
}
