import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeliveryDamascoComponent } from './delivery-damasco.component';

describe('DeliveryDamascoComponent', () => {
  let component: DeliveryDamascoComponent;
  let fixture: ComponentFixture<DeliveryDamascoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DeliveryDamascoComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeliveryDamascoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
