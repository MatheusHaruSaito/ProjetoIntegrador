import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { UserService } from '../../Services/user.service';
import { PostUserDto } from '../../models/PostUserDto';


@Component({
  selector: 'app-register',
  imports: [FormsModule,ReactiveFormsModule, RouterModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit {
  userForm!: FormGroup;
  user!: PostUserDto;

  constructor(private userService : UserService, private router: Router) {}
  ngOnInit(): void {
    this.userForm = new FormGroup({
      name: new FormControl("",Validators.required),
      password: new FormControl("",Validators.required),
      email: new FormControl("",[Validators.required,Validators.email]),
      ConfirmPassword: new FormControl("",Validators.required),
      description:new FormControl(""),
      cep:new FormControl(""),

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
      next: response =>{
        this.router.navigate(['/']);
      },
      error: error =>{

        this.AlertError(error.error);
      }
    });
  }
}

AlertError(message: string){
  window.alert(message);
}
}
