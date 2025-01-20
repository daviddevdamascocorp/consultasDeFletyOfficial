import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeliveryCanceladoComponent } from './delivery-cancelado.component';

describe('DeliveryCanceladoComponent', () => {
  let component: DeliveryCanceladoComponent;
  let fixture: ComponentFixture<DeliveryCanceladoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DeliveryCanceladoComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeliveryCanceladoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
