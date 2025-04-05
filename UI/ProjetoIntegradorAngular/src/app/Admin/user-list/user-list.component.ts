import { Component, OnInit } from '@angular/core';
import { User } from '../../models/user';
import { UserService } from '../../Services/user.service';

@Component({
  selector: 'app-user-list',
  imports: [],
  templateUrl: './user-list.component.html',
  styleUrl: './user-list.component.css'
})
export class UserListComponent implements OnInit {
  users: User[]= []

  constructor(private UserService : UserService) {}
  ngOnInit(): void {
    this.UserService.GetUsers().subscribe(response =>{
      this.users = response;
      console.log(response);
    })
  }

}
