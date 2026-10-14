import { Component } from '@angular/core';
import { DisplayCustomers } from '../display-customers/display-customers';
import { AddCustomer } from '../add-customer/add-customer';

@Component({
  imports: [DisplayCustomers, AddCustomer],
  selector: 'app-customers-page',
  styleUrl: './customers-page.scss',
  templateUrl: './customers-page.html',
})
export class CustomersPage {}
