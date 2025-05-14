import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { OngTicketService } from '../../Services/ong-ticket.service';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-ong-ticket-form',
  imports: [FormsModule,ReactiveFormsModule,RouterModule],
  templateUrl: './ong-ticket-form.component.html',
  styleUrl: './ong-ticket-form.component.css'
})
export class OngTicketFormComponent implements OnInit {

  constructor(private ongTicketService: OngTicketService, private router: Router) {

    
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
  OngTicketForm! : FormGroup;

  submit(){
    window.alert("Solicitação Enviada com Sucesso");
    this.ongTicketService.PostOngTicket(this.OngTicketForm.value).subscribe({
      next: response =>{
        this.router.navigate(['/'])
      },
      error: error =>{
        window.alert(error.error);
      }
    })
  }
}
