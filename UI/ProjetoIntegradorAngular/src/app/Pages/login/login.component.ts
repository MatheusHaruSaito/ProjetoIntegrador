import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../Services/auth.service';
import { LoginUser } from '../../models/LoginUser';
import { PopupService } from '../../Services/popup.service';

@Component({
  selector: 'app-login',
  imports: [FormsModule, ReactiveFormsModule, RouterModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {

  LoginForm!: FormGroup;
  userLogin!: LoginUser;
  constructor(private authService: AuthService, private router: Router, private popup: PopupService) { }
  ngOnInit(): void {
    this.LoginForm = new FormGroup({
      password: new FormControl("", Validators.required),
      email: new FormControl("", Validators.required)
    })
  }

  LogIn() {
    this.userLogin = this.LoginForm.value;
    this.authService.LogIn(this.userLogin).subscribe({
      next: response => {
        this.popup.show("Login realizado!");
      
        this.router.navigate(['/']);

      },
      error: err => {
        this.popup.show("Email ou senha incorretos!");
      }
    }
    )
  }
  ShowUserInfo() {
    var user = this.authService.GetUserFromJwtToken()
    window.alert("Logou como " + user.email);
  }

}
