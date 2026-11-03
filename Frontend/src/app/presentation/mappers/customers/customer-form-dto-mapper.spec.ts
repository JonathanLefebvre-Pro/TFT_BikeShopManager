import { TestBed } from '@angular/core/testing';
import { CustomerFormDtoMapper } from './customer-form-dto-mapper';

describe('CustomerFormDtoMapper', () => {
  let service: CustomerFormDtoMapper;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CustomerFormDtoMapper);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
