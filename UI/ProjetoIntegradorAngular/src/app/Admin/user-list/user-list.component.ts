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
  Allusers: User[]=[]
  constructor(private UserService : UserService) {}
  ngOnInit(): void {
    this.UserService.GetUsers().subscribe(response =>{
      this.users = response;
      this.Allusers = response;
      console.log(response);
    })
  }
  TriggerUserActive(id:string) :void{
    console.log(id);

    this.UserService.TriggerUserActive(id).subscribe(response =>{
      window.location.reload();

    })
  }
  Search(event: Event){
    const target = event.target as HTMLInputElement;
    const value = target.value.toLowerCase();
    console.log(value);
    this.users = this.Allusers.filter(user =>{
      return user.name.toLowerCase().includes(value);
    })
  }

}
