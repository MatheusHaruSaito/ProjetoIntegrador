import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../Services/auth.service';
import { LoginUser } from '../../models/LoginUser';

@Component({
  selector: 'app-login',
  imports: [FormsModule,ReactiveFormsModule, RouterModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {

  LoginForm!: FormGroup;
  userLogin!: LoginUser;
  constructor(private authService: AuthService, private router: Router) {}
  ngOnInit(): void {
    this.LoginForm = new FormGroup({
      password: new FormControl("",Validators.required),
      email: new FormControl("",Validators.required)
    })
  }

  LogIn(){
    this.userLogin = this.LoginForm.value;
    this.authService.LogIn(this.userLogin).subscribe({
        next: response =>{
               console.log("logado")
              var user =this.authService.GetUserFromToken(localStorage.getItem("JWT_TOKEN"))
              window.alert("Logou como "+user.email);
              window.location.reload();
          this.router.navigate(['/']);
        },
        error: err =>{
          console.log("Algo deu errado")
        }
      }
    )
  }


}
