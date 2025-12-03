import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InputDeviceOverviewComponent } from './input-device-overview.component';

describe('InputDeviceOverviewComponent', () => {
  let component: InputDeviceOverviewComponent;
  let fixture: ComponentFixture<InputDeviceOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InputDeviceOverviewComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(InputDeviceOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
