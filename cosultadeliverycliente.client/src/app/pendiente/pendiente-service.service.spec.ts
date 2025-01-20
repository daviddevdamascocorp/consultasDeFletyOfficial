import { TestBed } from '@angular/core/testing';

import { PendienteServiceService } from './pendiente-service.service';

describe('PendienteServiceService', () => {
  let service: PendienteServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PendienteServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
