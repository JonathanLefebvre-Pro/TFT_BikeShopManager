import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CustomerFormRow } from './customer-form-row';

describe('CustomerFormRow', () => {
  let component: CustomerFormRow;
  let fixture: ComponentFixture<CustomerFormRow>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CustomerFormRow],
    }).compileComponents();

    fixture = TestBed.createComponent(CustomerFormRow);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
