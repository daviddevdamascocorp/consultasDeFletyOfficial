import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeliveryEntregadoComponent } from './delivery-entregado.component';

describe('DeliveryEntregadoComponent', () => {
  let component: DeliveryEntregadoComponent;
  let fixture: ComponentFixture<DeliveryEntregadoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DeliveryEntregadoComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeliveryEntregadoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
