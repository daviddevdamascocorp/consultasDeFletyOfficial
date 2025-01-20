import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeliveryPendienteComponent } from './delivery-pendiente.component';

describe('DeliveryPendienteComponent', () => {
  let component: DeliveryPendienteComponent;
  let fixture: ComponentFixture<DeliveryPendienteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DeliveryPendienteComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeliveryPendienteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
