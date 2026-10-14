import { Component } from '@angular/core';
import { CustomersTable } from '../customers-table/customers-table';
import { AddCustomer } from '../add-customer/add-customer';
import { CustomerEditPanel } from '../customer-edit-panel/customer-edit-panel';

@Component({
  imports: [CustomersTable, AddCustomer, CustomerEditPanel],
  selector: 'app-customers-page',
  styleUrl: './customers-page.scss',
  templateUrl: './customers-page.html',
})
export class CustomersPage {}
