import { ComponentFixture, TestBed } from '@angular/core/testing';
import { DisplayStock } from './display-stock';

describe('DisplayStock', () => {
  let component: DisplayStock;
  let fixture: ComponentFixture<DisplayStock>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DisplayStock],
    }).compileComponents();

    fixture = TestBed.createComponent(DisplayStock);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
