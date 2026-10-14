import { Component, inject, input, signal } from '@angular/core';
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
  isEditModeEnabled = signal<boolean>(false);

  enableCustomer() {
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
    this.customerService.disableCustomer(this.customer().id).subscribe({
      next: (res) => {
        console.log("Result : " + JSON.stringify(res));
      },
      error(err) {
        console.log("Error" + err);
      },
    });
  }

  editCustomer() {
    this.isEditModeEnabled.set(true);
  }

  saveCustomer() {
    this.isEditModeEnabled.set(false);
  }
}
