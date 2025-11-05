import { Component, inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { UserService } from '../../Services/user.service';
import { AuthService } from '../../Services/auth.service';
import { UpdateUser } from '../../models/UpdateUser';

@Component({
  standalone: true,
  selector: 'app-edit-profile',
  imports: [RouterModule,FormsModule, ReactiveFormsModule],
  templateUrl: './edit-profile.component.html',
  styleUrl: './edit-profile.component.css'
})
export class EditProfileComponent implements OnInit {
  UpdateForm!: FormGroup;
  userService= inject(UserService)
  authService = inject(AuthService);
  selectedFile: File | null = null;
  route = inject(Router)
ngOnInit(): void {

  var userEmail = this.authService.GetUserFromJwtToken().email;
  this.userService.GetUsersByEmail(userEmail).subscribe({
    next: res =>{
        this.UpdateForm = new FormGroup({
          name: new FormControl(res.name),
          email: new FormControl(res.email),
          description: new FormControl(res.description),
          password: new FormControl(res.password),
          
        })
    }
  });
}
onFileSelected(event: Event) {
  const input = event.target as HTMLInputElement;
  if (input.files && input.files.length > 0) {
    this.selectedFile = input.files[0];
  }
}
  Update(): void{

      var userEmail = this.authService.GetUserFromJwtToken().email;
      this.userService.GetUsersByEmail(userEmail).subscribe({
        next: res =>{
          const updateUser : UpdateUser ={
            id: res.id,
            name : this.UpdateForm.get("name")?.value,
            email : this.UpdateForm.get("email")?.value,
            description: this.UpdateForm.get("description")?.value,
            password : this.UpdateForm.get("password")?.value,
            profileImg : this.selectedFile,
          }
              console.log(updateUser)
        this.userService.UpdateUser(updateUser).subscribe();
        this.route.navigate(["/Profile"]);
        }
      });



  }
  }
