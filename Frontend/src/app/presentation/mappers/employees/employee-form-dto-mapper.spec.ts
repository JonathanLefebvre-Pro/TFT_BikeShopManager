import { TestBed } from '@angular/core/testing';
import { EmployeeFormDtoMapper } from './employee-form-dto-mapper';

describe('EmployeeFormDtoMapper', () => {
  let service: EmployeeFormDtoMapper;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EmployeeFormDtoMapper);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
