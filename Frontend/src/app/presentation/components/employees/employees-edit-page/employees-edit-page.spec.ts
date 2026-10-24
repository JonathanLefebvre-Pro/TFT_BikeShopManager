import { ComponentFixture, TestBed } from '@angular/core/testing';
import { EmployeesEditPage } from './employees-edit-page';

describe('EmployeesEditPage', () => {
  let component: EmployeesEditPage;
  let fixture: ComponentFixture<EmployeesEditPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EmployeesEditPage],
    }).compileComponents();

    fixture = TestBed.createComponent(EmployeesEditPage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
