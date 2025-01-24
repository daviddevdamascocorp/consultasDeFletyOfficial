import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeliveryEntregadoDamascoComponent } from './delivery-entregado-damasco.component';

describe('DeliveryEntregadoDamascoComponent', () => {
  let component: DeliveryEntregadoDamascoComponent;
  let fixture: ComponentFixture<DeliveryEntregadoDamascoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DeliveryEntregadoDamascoComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeliveryEntregadoDamascoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
