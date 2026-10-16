import { Component, output, input } from '@angular/core';
import { CustomerTableRow } from '../customer-table-row/customer-table-row';
import { Customer } from '../../../../domain/customer';

@Component({
  imports: [CustomerTableRow],
  selector: 'app-customers-table',
  styleUrl: './customers-table.scss',
  templateUrl: './customers-table.html',
})
export class CustomersTable {
  readonly customers = input.required<Customer[]>();

  onEnableCustomerEvent = output<number>();
  onDisableCustomerEvent = output<number>();
  onEditCustomerEvent = output<number>();

  handleCustomerEnable(customerId: number){
    this.onEnableCustomerEvent.emit(customerId);
  }
  handleCustomerDisable(customerId: number){
    this.onDisableCustomerEvent.emit(customerId);
  }
  handleCustomerEdit(customerId: number) {
    this.onEditCustomerEvent.emit(customerId);
  }
}