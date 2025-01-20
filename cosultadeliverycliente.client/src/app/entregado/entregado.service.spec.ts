import { TestBed } from '@angular/core/testing';

import { EntregadoService } from './entregado.service';

describe('EntregadoService', () => {
  let service: EntregadoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EntregadoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
