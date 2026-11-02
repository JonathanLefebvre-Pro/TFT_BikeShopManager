import { Directive, input, signal } from '@angular/core';

@Directive({
  selector: '[appTextTooltip]',
  host: {
    '(mousenter)': 'displayTooltip()', 
    '(mousleave)': 'hideTooltip()',
    '[title]': 'tooltip()'
  }
})
export class TextTooltip {

  public tooltipInput = input<string>();
  private tooltip = signal<string>('');

  private displayTooltip() {
    this.tooltip.set(this.tooltipInput() ?? 'No tooltip');
  }

  private hideTooltip() {
    this.tooltip.set('');
  }
}
