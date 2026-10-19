import { Component, input } from '@angular/core';
import { FieldState, FieldTree, FormField } from '@angular/forms/signals';

@Component({
  imports: [FormField],
  selector: 'app-form-checkbox-input',
  styleUrl: './form-checkbox-input.scss',
  templateUrl: './form-checkbox-input.html',
})
export class FormCheckboxInput {
    label = input.required<string>();
    id = input.required<string>();
    field = input.required<FieldTree<boolean>>();
    fieldState = input.required<FieldState<boolean, string>>();
}
