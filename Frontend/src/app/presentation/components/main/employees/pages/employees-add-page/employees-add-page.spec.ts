import { ComponentFixture, TestBed } from '@angular/core/testing';
import { EmployeesAddPage } from './employees-add-page';

describe('EmployeesAddPage', () => {
  let component: EmployeesAddPage;
  let fixture: ComponentFixture<EmployeesAddPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EmployeesAddPage],
    }).compileComponents();

    fixture = TestBed.createComponent(EmployeesAddPage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
