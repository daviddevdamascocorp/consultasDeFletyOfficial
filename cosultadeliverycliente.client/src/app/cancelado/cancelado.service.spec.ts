import { TestBed } from '@angular/core/testing';

import { CanceladoService } from './cancelado.service';

describe('CanceladoService', () => {
  let service: CanceladoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CanceladoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
