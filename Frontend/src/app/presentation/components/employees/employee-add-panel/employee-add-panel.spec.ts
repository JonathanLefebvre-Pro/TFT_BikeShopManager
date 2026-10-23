import { ComponentFixture, TestBed } from '@angular/core/testing';
import { EmployeeAddPanel } from './employee-add-panel';

describe('EmployeeAddPanel', () => {
  let component: EmployeeAddPanel;
  let fixture: ComponentFixture<EmployeeAddPanel>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EmployeeAddPanel],
    }).compileComponents();

    fixture = TestBed.createComponent(EmployeeAddPanel);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
