import { Component, inject, input } from '@angular/core';
import { Customer } from '../../../../domain/customer';
import { CustomerService } from '@/app/infrastructure/services/customer-service';

@Component({
  imports: [],
  selector: 'app-customer-table-row',
  styleUrl: './customer-table-row.scss',
  templateUrl: './customer-table-row.html'
})
export class CustomerTableRow {

  customerService = inject(CustomerService);
  customer = input.required<Customer>();

  enableCustomer() {
    //console.log("Enable customer");
    this.customerService.enableCustomer(this.customer().id).subscribe({
      next: (res) => {
        console.log("Result : " + JSON.stringify(res));
      },
      error(err) {
        console.log("Error" + err);
      },
    });
  }

  disableCustomer() {
    //console.log("Disable customer");
    this.customerService.disableCustomer(this.customer().id).subscribe({
      next: (res) => {
        console.log("Result : " + JSON.stringify(res));
      },
      error(err) {
        console.log("Error" + err);
      },
    });
  }

  deleteCustomer() {
    console.log("Delete customer");
  }

  editCustomer() {
    console.log("Edit customer");
  }
}
