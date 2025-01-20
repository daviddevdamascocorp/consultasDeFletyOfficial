import { TestBed } from '@angular/core/testing';

import { EnCursoService } from './en-curso.service';

describe('EnCursoService', () => {
  let service: EnCursoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EnCursoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
