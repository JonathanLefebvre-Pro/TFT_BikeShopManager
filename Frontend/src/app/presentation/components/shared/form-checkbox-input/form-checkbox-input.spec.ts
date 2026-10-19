import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormCheckboxInput } from './form-checkbox-input';

describe('FormCheckboxInput', () => {
  let component: FormCheckboxInput;
  let fixture: ComponentFixture<FormCheckboxInput>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FormCheckboxInput],
    }).compileComponents();

    fixture = TestBed.createComponent(FormCheckboxInput);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
