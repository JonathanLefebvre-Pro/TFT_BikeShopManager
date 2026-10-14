import { Component, input } from '@angular/core';
import { FieldState, FieldTree, FormField } from '@angular/forms/signals';

@Component({
  imports: [FormField],
  selector: 'app-form-date-input',
  styleUrl: './form-date-input.scss',
  templateUrl: './form-date-input.html',
})
export class FormDateInput {
  label = input.required<string>();
  id = input.required<string>();
  field = input.required<FieldTree<Date | null>>();
  fieldState = input.required<FieldState<Date | null, string | number>>();
}
