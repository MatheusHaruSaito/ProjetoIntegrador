import { TestBed } from '@angular/core/testing';

import { OngTicketService } from './ong-ticket.service';

describe('OngTicketService', () => {
  let service: OngTicketService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OngTicketService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
