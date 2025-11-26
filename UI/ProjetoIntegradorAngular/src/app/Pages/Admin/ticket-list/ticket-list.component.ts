import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { OngTicket } from '../../../models/OngTicket';
import { OngTicketService } from '../../../Services/ong-ticket.service';
import { PopupService } from '../../../Services/popup.service';

@Component({
  selector: 'app-ticket-list',
  imports: [CommonModule],
  templateUrl: './ticket-list.component.html',
  styleUrls: ['./ticket-list.component.css']
})
export class TicketListComponent implements OnInit {
  ongTicketList: OngTicket[] = [];
  constructor(private ongTicketService: OngTicketService, private popup: PopupService) { }

  ngOnInit(): void {
    this.ongTicketService.GetOngTicketList().subscribe({
      next: response => {
        this.ongTicketList = response;
        console.log('Resposta recebida:', response);
      },
      error: err => {
        console.error('Erro na requisição:', err);
      }
    });
  }

  loadTickets() {
    this.ongTicketService.GetOngTicketList().subscribe({
      next: response => {
        this.ongTicketList = response;
      },
      error: err => {
        this.popup.show("Erro ao carregar tickets.");
      }
    });
  }

  AceeptTicket(id: string): void {
    this.ongTicketService.AcceptTicket(id).subscribe({
      next: () => {
        this.popup.show("Ticket aceito!");
        this.loadTickets();
      },
      error: error => {
        this.popup.show("Erro ao aceitar ticket.");
        console.log(error);
      }
    });
  }

  DeclineTicket(id: string): void {
    this.ongTicketService.DeclineTicket(id).subscribe({
      next: () => {
        this.popup.show("Ticket negado!");
        this.loadTickets();
      },
      error: error => {
        this.popup.show("Erro ao negar ticket.");
        console.log(error);
      }
    });
  }
}