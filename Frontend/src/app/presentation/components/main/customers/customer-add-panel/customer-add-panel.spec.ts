import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CustomerAddPanel } from './customer-add-panel';

describe('AddCustomer', () => {
  let component: CustomerAddPanel;
  let fixture: ComponentFixture<CustomerAddPanel>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CustomerAddPanel],
    }).compileComponents();

    fixture = TestBed.createComponent(CustomerAddPanel);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
