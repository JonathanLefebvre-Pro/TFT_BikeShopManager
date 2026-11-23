import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ErrorHandlerPage } from './error-handler-page';

describe('ErrorHandlerPage', () => {
  let component: ErrorHandlerPage;
  let fixture: ComponentFixture<ErrorHandlerPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ErrorHandlerPage],
    }).compileComponents();

    fixture = TestBed.createComponent(ErrorHandlerPage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
