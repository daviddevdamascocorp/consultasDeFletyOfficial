import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeliveryEnCursoComponent } from './delivery-en-curso.component';

describe('DeliveryEnCursoComponent', () => {
  let component: DeliveryEnCursoComponent;
  let fixture: ComponentFixture<DeliveryEnCursoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DeliveryEnCursoComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeliveryEnCursoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
