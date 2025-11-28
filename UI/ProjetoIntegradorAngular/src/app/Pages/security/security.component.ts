import { Component, inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { UserService } from '../../Services/user.service';
import { AuthService } from '../../Services/auth.service';
import { PopupService } from '../../Services/popup.service';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  selector: 'app-security',
  imports: [RouterModule, FormsModule, ReactiveFormsModule, CommonModule],
  templateUrl: './security.component.html',
  styleUrl: './security.component.css'
})
export class SecurityComponent implements OnInit {

  securityForm!: FormGroup;
  userService = inject(UserService);
  authService = inject(AuthService);
  popupService = inject(PopupService);
  router = inject(Router);

  ngOnInit(): void {
    this.securityForm = new FormGroup({
      currentPassword: new FormControl('', [Validators.required, Validators.minLength(6)]),
      newPassword: new FormControl('', [Validators.required, Validators.minLength(6)]),
      confirmPassword: new FormControl('', [Validators.required, Validators.minLength(6)]),
    });
  }

  updatePassword(): void {
    if (this.securityForm.invalid) {
      this.popupService.show('Por favor, preencha todos os campos corretamente.');
      return;
    }

    const formValue = this.securityForm.value;

    if (formValue.newPassword !== formValue.confirmPassword) {
      this.popupService.show('As senhas não coincidem.');
      return;
    }

    if (formValue.newPassword === formValue.currentPassword) {
      this.popupService.show('A nova senha não pode ser igual à senha atual.');
      return;
    }

    const updateData = {
      currentPassword: formValue.currentPassword,
      newPassword: formValue.newPassword
    };

    this.userService.UpdatePassword(updateData).subscribe({
      next: () => {
        this.popupService.show('Senha alterada com sucesso!');
        this.securityForm.reset();
        setTimeout(() => {
          this.router.navigate(['/Profile']);
        }, 1000);
      },
      error: (err) => {
        const errorMessage = err.error?.message || err.error || 'Erro ao alterar senha. Verifique se a senha atual está correta.';
        this.popupService.show(errorMessage);
      }
    });
  }
}