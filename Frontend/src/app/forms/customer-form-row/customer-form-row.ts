import { Component, input } from '@angular/core';
import { Customer } from '../../domain/customer';

@Component({
  imports: [],
  selector: 'app-customer-form-row',
  styleUrl: './customer-form-row.scss',
  templateUrl: './customer-form-row.html'
})
export class CustomerFormRow {
  customer = input.required<Customer>();
}
