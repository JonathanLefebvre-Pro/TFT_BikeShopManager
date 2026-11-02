import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormSelectInput } from './form-select-input';

describe('FormSelectInput', () => {
  let component: FormSelectInput;
  let fixture: ComponentFixture<FormSelectInput>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FormSelectInput],
    }).compileComponents();

    fixture = TestBed.createComponent(FormSelectInput);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
