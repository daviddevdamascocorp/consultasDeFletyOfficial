import { TestBed } from '@angular/core/testing';

import { FlotaDamascoService } from './flota-damasco.service';

describe('FlotaDamascoService', () => {
  let service: FlotaDamascoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FlotaDamascoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
