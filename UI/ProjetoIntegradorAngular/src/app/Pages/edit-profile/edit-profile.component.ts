import { Component, inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { UserService } from '../../Services/user.service';
import { AuthService } from '../../Services/auth.service';
import { UpdateUser } from '../../models/UpdateUser';
import { PopupService } from '../../Services/popup.service'; 
import { CommonModule } from '@angular/common';
import { UserProfileEditRequest } from '../../models/UserProfileEditRequest';

@Component({
  standalone: true,
  selector: 'app-edit-profile',
  imports: [RouterModule, FormsModule, ReactiveFormsModule, CommonModule],
  templateUrl: './edit-profile.component.html',
  styleUrl: './edit-profile.component.css'
})
export class EditProfileComponent implements OnInit {

  UpdateForm!: FormGroup;
  userService = inject(UserService);
  authService = inject(AuthService);
  popupService = inject(PopupService);   
  selectedFile: File | null = null;
  previewImage: string | null = null;  
  route = inject(Router);

  ngOnInit(): void {
    const userEmail = this.authService.GetUserFromJwtToken().email;

    this.userService.GetUsersByEmail(userEmail).subscribe({
      next: res => {
        this.UpdateForm = new FormGroup({
          name: new FormControl(res.name),
          description: new FormControl(res.description),
        });

        this.previewImage = res.profileImgPath;
      }
    });
  }

  onFileSelected(event: Event) {
    const input = event.target as HTMLInputElement;

    if (input.files && input.files.length > 0) {
      this.selectedFile = input.files[0];
      const reader = new FileReader();
      reader.onload = (e: any) => {
        this.previewImage = e.target.result;
      };
      reader.readAsDataURL(this.selectedFile);
    }
  }

  Update(): void {
    const userEmail = this.authService.GetUserFromJwtToken().email;

    this.userService.GetUsersByEmail(userEmail).subscribe({
      next: res => {
        const updateUser: UserProfileEditRequest = {
          id: this.authService.GetUserFromJwtToken().id,
          name: this.UpdateForm.get("name")?.value,
          email: res.email,
          description: this.UpdateForm.get("description")?.value,
          profileImg: this.selectedFile,
          profileImgPath: ''
        };

        this.userService.EditProfile(updateUser).subscribe({
          next: () => {
            this.popupService.show("Perfil atualizado com sucesso!");
            this.authService.RefreshSession(); 
            setTimeout(() => {
              this.route.navigate(["/Profile"]);
            }, 600);
          },
          error: (err) => {
            const errorMessage = err.error || err.error || 'Erro ao atualizar perfil.';
            this.popupService.show(errorMessage);
          }
        });
      }
    });
  }
}