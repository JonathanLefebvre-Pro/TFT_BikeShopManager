import { ComponentFixture, TestBed } from '@angular/core/testing';
import { UserFormRow } from './user-form-row';

describe('UserFormRow', () => {
  let component: UserFormRow;
  let fixture: ComponentFixture<UserFormRow>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UserFormRow],
    }).compileComponents();

    fixture = TestBed.createComponent(UserFormRow);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
