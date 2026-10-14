import { Component, input } from '@angular/core';
import { Customer } from '../../../domain/customer';

@Component({
  imports: [],
  selector: 'app-customer-table-row',
  styleUrl: './customer-table-row.scss',
  templateUrl: './customer-table-row.html'
})
export class CustomerTableRow {
  customer = input.required<Customer>();
}
