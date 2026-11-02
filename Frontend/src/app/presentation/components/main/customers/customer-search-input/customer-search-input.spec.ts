import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CustomerSearchInput } from './customer-search-input';

describe('CustomerSearchInput', () => {
  let component: CustomerSearchInput;
  let fixture: ComponentFixture<CustomerSearchInput>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CustomerSearchInput],
    }).compileComponents();

    fixture = TestBed.createComponent(CustomerSearchInput);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
