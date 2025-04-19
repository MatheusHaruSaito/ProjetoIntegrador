import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { UserService } from '../../Services/user.service';
import { PostUserDto } from '../../models/PostUserDto';


@Component({
  selector: 'app-register-form',
  imports: [FormsModule,ReactiveFormsModule, RouterModule],
  templateUrl: './register-form.component.html',
  styleUrl: './register-form.component.css'
})
export class RegisterFormComponent implements OnInit {
  @Output() onSubmit = new EventEmitter<PostUserDto>();
  userForm!: FormGroup;
  user!: PostUserDto;

  constructor(private userService : UserService, private router: Router) {}
  ngOnInit(): void {
    this.userForm = new FormGroup({
      name: new FormControl("",Validators.required),
      password: new FormControl("",Validators.required),
      email: new FormControl("",[Validators.required,Validators.required]),
      ConfirmPassword: new FormControl("",Validators.required)
    })
  }
 Register(): void{
  
  this.user = this.userForm.value;

  if(this.user.name == ""){
    this.AlertError("Name is Empty");
  }
  else if(this.user.email == ""){
    this.AlertError("Email is Empty");
  }
  else if(this.user.password == ""){
    this.AlertError("Pasword is Empty");
  }
  else{
    this.userService.PostUser(this.user).subscribe({
      next: respose =>{
        this.router.navigate(['/']);
      },
      error: error =>{

        this.AlertError(error.error);
      }
    });
}
  
 }
 PostUser(user: PostUserDto){

}
AlertError(message: string){
  window.alert(message);
}
 
}
