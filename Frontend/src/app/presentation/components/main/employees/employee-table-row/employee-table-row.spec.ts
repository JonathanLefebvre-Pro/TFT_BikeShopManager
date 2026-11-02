import { ComponentFixture, TestBed } from '@angular/core/testing';
import { EmployeeTableRow } from './employee-table-row';

describe('EmployeeTableRow', () => {
  let component: EmployeeTableRow;
  let fixture: ComponentFixture<EmployeeTableRow>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EmployeeTableRow],
    }).compileComponents();

    fixture = TestBed.createComponent(EmployeeTableRow);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
