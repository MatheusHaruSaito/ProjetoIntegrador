import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { UserService } from '../../Services/user.service';
import { PostUserDto } from '../../models/PostUserDto';
import { RegisterFormComponent } from "../register-form/register-form.component";

@Component({
  selector: 'app-register',
  imports: [RouterModule, RegisterFormComponent],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {


  constructor(private userService: UserService, private router : Router) {
    
  }
  PostUser(user: PostUserDto){
    this.userService.PostUser(user).subscribe(response =>{
      console.log(response);
      this.router.navigate(['/']);

    });
  }
}
