import { TestBed } from '@angular/core/testing';
import { CustomerDtoMapper } from './customer-dto-mapper';

describe('CustomerDtoMapper', () => {
  let service: CustomerDtoMapper;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CustomerDtoMapper);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
