import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
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
  constructor(private userService : UserService) {}
  ngOnInit(): void {
    this.userForm = new FormGroup({
      name: new FormControl(""),
      password: new FormControl(""),
      email: new FormControl("")
    })
  }

 Register(): void{

  this.onSubmit.emit(this.userForm.value);

 }
 
}
