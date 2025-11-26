import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { OngTicketService } from '../../Services/ong-ticket.service';
import { Router, RouterModule } from '@angular/router';
import { PopupService } from '../../Services/popup.service';

@Component({
  selector: 'app-ong-ticket-form',
  imports: [FormsModule, ReactiveFormsModule, RouterModule],
  templateUrl: './ong-ticket-form.component.html',
  styleUrl: './ong-ticket-form.component.css'
})
export class OngTicketFormComponent implements OnInit {

  constructor(private ongTicketService: OngTicketService, private router: Router, private popup: PopupService) {


  }
  ngOnInit(): void {
    this.OngTicketForm = new FormGroup({
      Description: new FormControl(""),
      Name: new FormControl(""),
      Email: new FormControl(""),
      Cep: new FormControl(""),
      Cnpj: new FormControl(""),
    });
  }
  OngTicketForm!: FormGroup;

  submit() {
    this.ongTicketService.PostOngTicket(this.OngTicketForm.value).subscribe({
      next: () => {
        this.popup.show("Solicitação enviada com sucesso!");
        this.router.navigate(['/']);
      },
      error: err => {
        this.popup.show(err.error || "Erro ao enviar solicitação.");
      }
    });
  }
}
