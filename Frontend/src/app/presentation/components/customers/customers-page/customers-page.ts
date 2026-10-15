import { Component, signal } from '@angular/core';
import { CustomersTable } from '../customers-table/customers-table';
import { AddCustomer } from '../add-customer/add-customer';
import { CustomerEditPanel } from '../customer-edit-panel/customer-edit-panel';
import { Customer } from '@/app/domain/customer';

@Component({
  imports: [CustomersTable, AddCustomer, CustomerEditPanel],
  selector: 'app-customers-page',
  styleUrl: './customers-page.scss',
  templateUrl: './customers-page.html',
})
export class CustomersPage {

  lastEditedCustomer = signal<Customer | null>(null);

  handleEditCustomer(customer: Customer) {
    if(customer !== null)
    {
      this.lastEditedCustomer.set(customer);
      console.log("Edit customer - Event from Customers Page for : " + this.lastEditedCustomer()?.id);
    }
  }

  handleSaveCustomer() {
    this.lastEditedCustomer.set(null);
    console.log("Save customer - Event from Customers Page")
  }
}
