import { Component, OnInit } from '@angular/core';
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
  ) {}

  ngOnInit(): void {
    this.userForm = new FormGroup({
      name: new FormControl('', Validators.required),
      password: new FormControl('', [
        Validators.required,
        Validators.minLength(6),
      ]),
      email: new FormControl('', [Validators.required, Validators.email]),
      ConfirmPassword: new FormControl('', Validators.required),
      description: new FormControl(''),
      cep: new FormControl(''),
    });
  }

  Register(): void {
    if (this.userForm.invalid) {
      this.popup.show(
        'Por favor, preencha todos os campos obrigatórios corretamente.'
      );
      return;
    }

    this.user = this.userForm.value;

    if (this.user.password !== this.user.ConfirmPassword) {
      this.popup.show('As senhas não coincidem.');
      return;
    }

    this.userService.PostUser(this.user).subscribe({
      next: () => {
        this.popup.show('Conta criada com sucesso!');
        this.router.navigate(['/Login']);
      },
      error: (err) => {
          this.popup.show(err.error?.message  || 'Erro ao criar conta.');
      },
    });
  }


}
