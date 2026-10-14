import { ComponentFixture, TestBed } from '@angular/core/testing';
import { DisplayShop } from './display-shop';

describe('DisplayShop', () => {
  let component: DisplayShop;
  let fixture: ComponentFixture<DisplayShop>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DisplayShop],
    }).compileComponents();

    fixture = TestBed.createComponent(DisplayShop);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
