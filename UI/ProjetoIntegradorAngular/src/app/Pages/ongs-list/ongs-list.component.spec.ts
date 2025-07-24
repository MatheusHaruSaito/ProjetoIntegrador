import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ONGsListComponent } from './ongs-list.component';

describe('ONGsListComponent', () => {
  let component: ONGsListComponent;
  let fixture: ComponentFixture<ONGsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ONGsListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ONGsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
