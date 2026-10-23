import { Directive, signal } from '@angular/core';

@Directive({
  selector: '[appColorButton]',
  host: {
    '(mousenter)': 'color.set("red")', 
    '(mousleave)': 'color.set("blue")', 
    '[style.backgroundColor]': 'color()',
  }
})
export class ColorButton {
  color = signal('Red');
  stock = signal<number>(0);
}
