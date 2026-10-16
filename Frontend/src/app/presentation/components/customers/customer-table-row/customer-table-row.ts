import { Component, input, OnChanges, output, signal, SimpleChanges } from '@angular/core';
import { Customer } from '../../../../domain/customer';
import { DatePipe } from '@angular/common';

@Component({
  imports: [DatePipe],
  selector: 'app-customer-table-row',
  styleUrl: './customer-table-row.scss',
  templateUrl: './customer-table-row.html'
})
export class CustomerTableRow implements OnChanges {
  
  customer = input.required<Customer>();
  isEditModeEnabled = signal<boolean>(false);
  
  onEnableCustomerEvent = output<number>();
  onDisableCustomerEvent = output<number>();
  onEditCustomerEvent = output<number>();

  ngOnChanges(changes: SimpleChanges): void {
    this.isEditModeEnabled.set(false);
  }

  handleEnableCustomer() {
    this.onEnableCustomerEvent.emit(this.customer().id);
  }

  handleDisableCustomer() {
    this.onDisableCustomerEvent.emit(this.customer().id);
  }

  handleEditCustomer() {
    this.isEditModeEnabled.set(true);
    this.onEditCustomerEvent.emit(this.customer().id);
  }

  disableEditMode() {
    this.isEditModeEnabled.set(true);
  }
  enableEditMode() {
    this.isEditModeEnabled.set(false);
  }
}
