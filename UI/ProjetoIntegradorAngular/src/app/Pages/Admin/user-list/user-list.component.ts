import { Component, OnInit } from '@angular/core';
import { UserService } from '../../../Services/user.service';
import { CommonModule } from '@angular/common';
import { User } from '../../../models/user';


@Component({
  selector: 'app-user-list',
  imports: [CommonModule],
  templateUrl: './user-list.component.html',
  styleUrl: './user-list.component.css'
})
export class UserListComponent implements OnInit {
  users: User[]= []
  Allusers: User[]=[]
  constructor(private UserService : UserService) {}
ngOnInit(): void {
  this.UserService.GetUsers().subscribe(response => {
    this.Allusers = response.filter(user => {
      const isAdmin = user.role.some(r => r.toLowerCase() === 'admin');
      return !isAdmin;
    });
    this.users = [...this.Allusers];
      console.log(this.Allusers)

  });
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
      return user.name.toLowerCase().includes(value)||
              user.role.includes(value)
    })
  }

}
