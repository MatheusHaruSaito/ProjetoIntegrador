import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { ViewUserPost } from '../../models/ViewUserPost';
import { UserPostService } from '../../Services/user-post.service';
import { response } from 'express';

@Component({
  selector: 'app-feed',
  imports: [CommonModule],
  templateUrl: './feed.component.html',
  styleUrl: './feed.component.css'
})
export class FeedComponent implements OnInit {
    posts: ViewUserPost[]= []
    postService = inject(UserPostService)
  ngOnInit(): void {
    this.postService.GetAll().subscribe(response =>{
      this.posts = response;
      console.log(this.posts)
    })
  }

}
