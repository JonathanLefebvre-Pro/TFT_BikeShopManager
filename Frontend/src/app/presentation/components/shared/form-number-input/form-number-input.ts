import { Component, input } from '@angular/core';
import { FieldState, FieldTree, FormField } from '@angular/forms/signals';

@Component({
  imports: [FormField],
  selector: 'app-form-number-input',
  styleUrl: './form-number-input.scss',
  templateUrl: './form-number-input.html',
})
export class FormNumberInput {
  label = input.required<string>();
  id = input.required<string>();
  field = input.required<FieldTree<number>>();
  fieldState = input.required<FieldState<number, string>>();
}