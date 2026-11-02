import { TestBed } from '@angular/core/testing';
import { RoleDtoMapper } from './role-dto-mapper';

describe('RoleDtoMapper', () => {
  let service: RoleDtoMapper;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RoleDtoMapper);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
