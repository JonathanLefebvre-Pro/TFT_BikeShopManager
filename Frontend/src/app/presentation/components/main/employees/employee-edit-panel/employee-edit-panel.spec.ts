import { ComponentFixture, TestBed } from '@angular/core/testing';
import { EmployeeEditPanel } from './employee-edit-panel';

describe('EmployeeEditPanel', () => {
  let component: EmployeeEditPanel;
  let fixture: ComponentFixture<EmployeeEditPanel>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EmployeeEditPanel],
    }).compileComponents();

    fixture = TestBed.createComponent(EmployeeEditPanel);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
