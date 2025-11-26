import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { UserService } from '../../Services/user.service';
import { PostUserDto } from '../../models/PostUserDto';
import { PopupService } from '../../Services/popup.service';

@Component({
  selector: 'app-register',
  imports: [FormsModule, ReactiveFormsModule, RouterModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent implements OnInit {
  userForm!: FormGroup;
  user!: PostUserDto;

  constructor(
    private userService: UserService,
    private router: Router,
    private popup: PopupService
  ) { }
  ngOnInit(): void {
    this.userForm = new FormGroup({
      name: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required),
      email: new FormControl('', [Validators.required, Validators.email]),
      ConfirmPassword: new FormControl('', Validators.required),
      description: new FormControl(''),
      cep: new FormControl(''),
    });
  }
  Register(): void {
    this.user = this.userForm.value;

    if (this.user.name == '') {
      this.popup.show('O nome não pode estar vazio.');
    } else if (this.user.email == '') {
      this.popup.show('O email não pode estar vazio.');
    } else if (this.user.password == '') {
      this.popup.show('A senha não pode estar vazia.');
    } else if (this.user.password !== this.user.ConfirmPassword) {
      this.popup.show('As senhas não coincidem.');
    } else {
      this.userService.PostUser(this.user).subscribe({
        next: () => {
          this.popup.show('Conta criada com sucesso!');
          this.router.navigate(['/Login']);
        },
        error: (err) => {
          this.popup.show(err.error || 'Erro ao criar conta.');
        },
      });
    }
  }
}
