import { Component, output, input, signal, viewChildren } from '@angular/core';
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

  protected readonly customerTableRowElements = viewChildren(CustomerTableRow);

  handleCustomerEnable(customerId: number){
    this.onEnableCustomerEvent.emit(customerId);
  }
  handleCustomerDisable(customerId: number){
    this.onDisableCustomerEvent.emit(customerId);
  }
  handleCustomerEdit(customerId: number) {
    this.onEditCustomerEvent.emit(customerId);
  }

  notifyEditModeEnabled() {
    if(this.customerTableRowElements() !== null && 
      this.customerTableRowElements().length > 0)
      {
        for (let index = 0; index < this.customerTableRowElements().length; index++) {
          const element = this.customerTableRowElements()[index];
          element.disableEditMode();
        }
      }
  }
  notifyEditModeDisabled() {
    if(this.customerTableRowElements() !== null && 
      this.customerTableRowElements().length > 0)
      {
        for (let index = 0; index < this.customerTableRowElements().length; index++) {
          const element = this.customerTableRowElements()[index];
          if(element.customer().isActive)
            element.enableEditMode();
        }
      }
  }
}