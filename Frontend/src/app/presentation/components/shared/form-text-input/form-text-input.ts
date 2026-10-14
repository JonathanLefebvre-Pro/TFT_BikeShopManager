import { Component, input } from '@angular/core';
import { FieldState, FieldTree, FormField } from '@angular/forms/signals';

@Component({
  imports: [FormField],
  selector: 'app-form-text-input',
  styleUrl: './form-text-input.scss',
  templateUrl: './form-text-input.html',
})
export class FormTextInput {
  label = input.required<string>();
  id = input.required<string>();
  field = input.required<FieldTree<string>>();
  fieldState = input.required<FieldState<string, string>>();
  type = input<string>("text");
}
