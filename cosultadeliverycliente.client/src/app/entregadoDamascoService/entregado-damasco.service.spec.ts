import { TestBed } from '@angular/core/testing';

import { EntregadoDamascoService } from './entregado-damasco.service';

describe('EntregadoDamascoService', () => {
  let service: EntregadoDamascoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EntregadoDamascoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
