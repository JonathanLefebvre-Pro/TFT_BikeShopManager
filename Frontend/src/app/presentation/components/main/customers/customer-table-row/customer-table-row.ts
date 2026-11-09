import { Component, input, OnChanges, output, signal, SimpleChanges } from '@angular/core';
import { Customer } from '@domain/customer';
import { DatePipe } from '@angular/common';
import { EmptyStringPlaceholderPipe } from '@/app/appCore/pipes/empty-string-placeholder-pipe';
import { matAccountCircle, matEdit, matNoAccounts } from '@ng-icons/material-icons/baseline';
import { NgIcon, provideIcons } from '@ng-icons/core';

@Component({
  imports: [DatePipe, EmptyStringPlaceholderPipe, NgIcon],
  selector: 'app-customer-table-row',
  styleUrl: './customer-table-row.scss',
  templateUrl: './customer-table-row.html',
  providers: [provideIcons({matNoAccounts, matAccountCircle, matEdit})]
})
export class CustomerTableRow implements OnChanges {
  
  public customer = input.required<Customer>();
  protected isEditModeEnabled = signal<boolean>(false);
  protected displayDetails = signal<boolean>(false);
  
  public onEnableCustomerEvent = output<number>();
  public onDisableCustomerEvent = output<number>();
  public onEditCustomerEvent = output<number>();
  public onDisplayDetailsCustomerEvent = output<number>();

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

  protected handleDisplayDetails() {
    this.onDisplayDetailsCustomerEvent.emit(this.customer().id);
    this.displayDetails.set(!this.displayDetails());
  }

  public disableEditMode() {
    this.isEditModeEnabled.set(true);
  }
  public enableEditMode() {
    this.isEditModeEnabled.set(false);
  }
  public hideDetailsPanel() {
    this.displayDetails.set(false);
  }
}
