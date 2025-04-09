import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { UserService } from '../../Services/user.service';
import { PostUserDto } from '../../models/PostUserDto';

@Component({
  selector: 'app-register',
  imports: [FormsModule, RouterModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  constructor(private userService : UserService) {}
 Register(user : PostUserDto): void{
  this.userService.PostUser(user).subscribe((response) => {
    console.log(response);
  }
 }
 
}
