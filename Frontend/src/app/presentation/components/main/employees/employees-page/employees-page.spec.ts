import { ComponentFixture, TestBed } from '@angular/core/testing';
import { DisplayEmployees } from './display-employees';

describe('DisplayEmployees', () => {
  let component: DisplayEmployees;
  let fixture: ComponentFixture<DisplayEmployees>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DisplayEmployees],
    }).compileComponents();

    fixture = TestBed.createComponent(DisplayEmployees);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
