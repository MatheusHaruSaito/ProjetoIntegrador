import { Component, inject } from '@angular/core';
import { EmailService } from '../../Services/email.service';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CreateEmail } from '../../models/CreateEmail';
import { PopupService } from '../../Services/popup.service';
import { CommonModule } from '@angular/common';
import { ProfilemenuComponent } from '../../pageComponent/profilemenu/profilemenu.component';

@Component({
  selector: 'app-feedback',
  imports: [FormsModule, ReactiveFormsModule, CommonModule, ProfilemenuComponent],
  templateUrl: './feedback.component.html',
  styleUrl: './feedback.component.css'
})
export class FeedbackComponent {

  feedbackFroms!: FormGroup;
  emailRequest!: CreateEmail;

  emailService = inject(EmailService);
  popup = inject(PopupService);

  SendFeedBack() {
    this.emailRequest = this.feedbackFroms.value;
    this.emailRequest.toEmail = "unolinkfeedback@gmail.com";

    this.emailService.SendEmail(this.emailRequest).subscribe({
      next: () => {
        this.popup.show("Email enviado!");
      }
    });
  }

  ngOnInit() {
    this.feedbackFroms = new FormGroup({
      subject: new FormControl("", Validators.required),
      body: new FormControl("", Validators.required)
    });
  }
}
