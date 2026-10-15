import { Component, inject, input, output, signal } from '@angular/core';
import { Customer } from '../../../../domain/customer';
import { CustomerService } from '@/app/infrastructure/services/customer-service';
import { DatePipe } from '@angular/common';

@Component({
  imports: [DatePipe],
  selector: 'app-customer-table-row',
  styleUrl: './customer-table-row.scss',
  templateUrl: './customer-table-row.html'
})
export class CustomerTableRow {

  customerService = inject(CustomerService);
  customer = input.required<Customer>();
  isEditModeEnabled = signal<boolean>(false);

  onEditCustomerEvent = output<Customer>();
  onSaveCustomerEvent = output();

  handleEnableCustomer() {
    this.customerService.enableCustomer(this.customer().id).subscribe({
      next: (res) => {
        console.log("Result : " + JSON.stringify(res));
      },
      error(err) {
        console.log("Error" + err);
      },
    });
  }

  handleDisableCustomer() {
    this.customerService.disableCustomer(this.customer().id).subscribe({
      next: (res) => {
        console.log("Result : " + JSON.stringify(res));
      },
      error(error) {
        console.error("Error" + error);
      },
    });
  }

  handleEditCustomer() {
    this.isEditModeEnabled.set(true);
    this.onEditCustomerEvent.emit(this.customer());
  }

  handleSaveCustomer() {
    this.isEditModeEnabled.set(false);
    this.onSaveCustomerEvent.emit();
  }
}
