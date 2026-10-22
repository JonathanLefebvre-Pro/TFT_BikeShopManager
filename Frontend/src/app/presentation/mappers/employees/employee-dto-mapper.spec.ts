import { TestBed } from '@angular/core/testing';
import { EmployeeDtoMapper } from './employee-dto-mapper';

describe('EmployeeDtoMapper', () => {
  let service: EmployeeDtoMapper;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EmployeeDtoMapper);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
