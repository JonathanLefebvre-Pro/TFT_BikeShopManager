import { Component, input, OnChanges, output, signal, SimpleChanges } from '@angular/core';
import { Customer } from '@domain/customer';
import { DatePipe } from '@angular/common';

@Component({
  imports: [DatePipe],
  selector: 'app-customer-table-row',
  styleUrl: './customer-table-row.scss',
  templateUrl: './customer-table-row.html'
})
export class CustomerTableRow implements OnChanges {
  
  public customer = input.required<Customer>();
  protected isEditModeEnabled = signal<boolean>(false);
  
  public onEnableCustomerEvent = output<number>();
  public onDisableCustomerEvent = output<number>();
  public onEditCustomerEvent = output<number>();

  public ngOnChanges(changes: SimpleChanges): void {
    this.isEditModeEnabled.set(false);
  }

  protected handleEnableCustomer() {
    this.onEnableCustomerEvent.emit(this.customer().id);
  }

  protected handleDisableCustomer() {
    this.onDisableCustomerEvent.emit(this.customer().id);
  }

  protected handleEditCustomer() {
    this.isEditModeEnabled.set(true);
    this.onEditCustomerEvent.emit(this.customer().id);
  }

  public disableEditMode() {
    this.isEditModeEnabled.set(true);
  }
  public enableEditMode() {
    this.isEditModeEnabled.set(false);
  }
}
