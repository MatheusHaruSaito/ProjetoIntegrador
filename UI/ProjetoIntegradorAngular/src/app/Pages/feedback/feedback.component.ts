import { Component, inject } from '@angular/core';
import { EmailService } from '../../Services/email.service';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CreateEmail } from '../../models/CreateEmail';
import { HomeComponent } from "../home/home.component";
import { PopupComponent } from "../../pageComponent/popup/popup.component";
import { CommonModule } from '@angular/common';
import { ProfilemenuComponent } from '../../pageComponent/profilemenu/profilemenu.component';

@Component({
  selector: 'app-feedback',
  imports: [FormsModule, ReactiveFormsModule, PopupComponent, CommonModule, ProfilemenuComponent],
  templateUrl: './feedback.component.html',
  styleUrl: './feedback.component.css'
})
export class FeedbackComponent{
  popupAberto: boolean = false
  feedbackFroms!: FormGroup
  emailRequest!: CreateEmail
  emailService = inject(EmailService)
  SendFeedBack(){
    this.emailRequest = this.feedbackFroms.value
    this.emailRequest.toEmail = "unolinkfeedback@gmail.com"
        console.log(this.emailRequest)
    this.emailService.SendEmail(this.emailRequest).subscribe({
      next: r =>{
        this.popupAberto = true
      }
    })
  }

  ngOnInit(){
    this.feedbackFroms = new FormGroup({
      subject: new FormControl("",Validators.required),
      body: new FormControl("",Validators.required)
    })
  }
}

