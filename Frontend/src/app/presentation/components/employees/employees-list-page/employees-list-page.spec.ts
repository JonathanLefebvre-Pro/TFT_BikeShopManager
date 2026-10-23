import { ComponentFixture, TestBed } from '@angular/core/testing';
import { EmployeesListPage } from './employees-list-page';

describe('EmployeesListPage', () => {
  let component: EmployeesListPage;
  let fixture: ComponentFixture<EmployeesListPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EmployeesListPage],
    }).compileComponents();

    fixture = TestBed.createComponent(EmployeesListPage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
