import { ComponentFixture, TestBed } from '@angular/core/testing';
import { EmployeeProfileEditPanel } from './employee-profile-edit-panel';

describe('EmployeeProfileEditPanel', () => {
  let component: EmployeeProfileEditPanel;
  let fixture: ComponentFixture<EmployeeProfileEditPanel>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EmployeeProfileEditPanel],
    }).compileComponents();

    fixture = TestBed.createComponent(EmployeeProfileEditPanel);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
