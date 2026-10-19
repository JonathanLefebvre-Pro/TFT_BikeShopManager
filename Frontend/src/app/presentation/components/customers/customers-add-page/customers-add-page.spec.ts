import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CustomersAddPage } from './customers-add-page';

describe('CustomersAddPage', () => {
  let component: CustomersAddPage;
  let fixture: ComponentFixture<CustomersAddPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CustomersAddPage],
    }).compileComponents();

    fixture = TestBed.createComponent(CustomersAddPage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
