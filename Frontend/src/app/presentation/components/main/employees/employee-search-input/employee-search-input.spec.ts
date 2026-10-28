import { ComponentFixture, TestBed } from '@angular/core/testing';
import { EmployeeSearchInput } from './employee-search-input';

describe('EmployeeSearchInput', () => {
  let component: EmployeeSearchInput;
  let fixture: ComponentFixture<EmployeeSearchInput>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EmployeeSearchInput],
    }).compileComponents();

    fixture = TestBed.createComponent(EmployeeSearchInput);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
