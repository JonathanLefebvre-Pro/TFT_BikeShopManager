import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormNumberInput } from './form-number-input';

describe('FormNumberInput', () => {
  let component: FormNumberInput;
  let fixture: ComponentFixture<FormNumberInput>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FormNumberInput],
    }).compileComponents();

    fixture = TestBed.createComponent(FormNumberInput);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
