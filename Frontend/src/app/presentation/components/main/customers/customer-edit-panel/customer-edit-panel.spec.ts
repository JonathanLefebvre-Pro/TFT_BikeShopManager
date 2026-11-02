import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CustomerEditPanel } from './customer-edit-panel';

describe('CustomerEditPanel', () => {
  let component: CustomerEditPanel;
  let fixture: ComponentFixture<CustomerEditPanel>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CustomerEditPanel],
    }).compileComponents();

    fixture = TestBed.createComponent(CustomerEditPanel);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
