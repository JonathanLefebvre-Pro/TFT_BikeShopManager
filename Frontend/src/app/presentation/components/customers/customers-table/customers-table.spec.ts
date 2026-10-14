import { ComponentFixture, TestBed } from '@angular/core/testing';
import { DisplayCustomers } from './customers-table';

describe('DisplayCustomers', () => {
  let component: DisplayCustomers;
  let fixture: ComponentFixture<DisplayCustomers>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DisplayCustomers],
    }).compileComponents();

    fixture = TestBed.createComponent(DisplayCustomers);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
