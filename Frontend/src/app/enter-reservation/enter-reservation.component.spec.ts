import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EnterReservationComponent } from './process-order.component';

describe('EnterReservationComponent', () => {
  let component: EnterReservationComponent;
  let fixture: ComponentFixture<EnterReservationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EnterReservationComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EnterReservationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
