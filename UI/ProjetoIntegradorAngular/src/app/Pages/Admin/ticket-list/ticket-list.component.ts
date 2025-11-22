import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { OngTicket } from '../../../models/OngTicket';
import { OngTicketService } from '../../../Services/ong-ticket.service';


@Component({
  selector: 'app-ticket-list',
  imports: [CommonModule],
  templateUrl: './ticket-list.component.html',
 styleUrls: ['./ticket-list.component.css']
})
export class TicketListComponent implements OnInit {
  ongTicketList: OngTicket[] =[];
  constructor(private ongTicketService: OngTicketService){}
  ngOnInit(): void {
  this.ongTicketService.GetOngTicketList().subscribe({
    next: response => {
      this.ongTicketList = response.filter(x => {x.reviwed = false});
      console.log('Resposta recebida:', response);
    },
    error: err => {
      console.error('Erro na requisição:', err);
    }
  });
  }
  AceeptTicket(id:string):void{
    this.ongTicketService.AcceptTicket(id).subscribe({
      next: response=>{
        window.alert("Ticket Aceito");
        window.location.reload();
      },
      error: error=>{
        console.log(error);
      }
    })
  }
  DeclineTicket(id:string):void{
    this.ongTicketService.DeclineTicket(id).subscribe({
      next: response=>{
        window.alert("Ticket Negado");
        window.location.reload();
      },
      error: error=>{
        console.log(error);
      }
    })
  }
}
