import { Component, input, model } from '@angular/core';
import { FieldState, FormValueControl } from '@angular/forms/signals';

@Component({
  imports: [],
  selector: 'app-form-select-input',
  styleUrl: './form-select-input.scss',
  templateUrl: './form-select-input.html',
})
export class FormSelectInput {  
  label = input.required<string>();
  id = input.required<string>();
  fieldState = input.required<FieldState<number, string>>();
  options = input.required<string[]>();
}
