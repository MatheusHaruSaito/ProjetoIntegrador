import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OngTicketFormComponent } from './ong-ticket-form.component';

describe('OngTicketFormComponent', () => {
  let component: OngTicketFormComponent;
  let fixture: ComponentFixture<OngTicketFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OngTicketFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OngTicketFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
