import { Component, output, input, viewChildren, signal } from '@angular/core';
import { CustomerTableRow } from '@presentation/components/main/customers/customer-table-row/customer-table-row';
import { Customer } from '@domain/customer';

@Component({
  imports: [CustomerTableRow],
  selector: 'app-customers-table',
  styleUrl: './customers-table.scss',
  templateUrl: './customers-table.html',
})
export class CustomersTable {
  public readonly customers = input.required<Customer[]>();
  public readonly currentPageNumber = input.required<number>();
  public readonly maxPagesNumber = input.required<number>();

  public onEnableCustomerEvent = output<number>();
  public onDisableCustomerEvent = output<number>();
  public onEditCustomerEvent = output<number>();
  public onNextPageEvent = output();
  public onPreviousPageEvent = output();

  private readonly defaultErrorMessage = "There is nothing to display !";
  protected readonly errorMessage = signal<string>(this.defaultErrorMessage);
  
  protected readonly customerTableRowElements = viewChildren(CustomerTableRow);

  private readonly INITIAL_HEADERS : string[] = [
    '#',
    'FirstName', 
    'LastName', 
    'Email',
    'IsActive',
    'Actions'
  ];
  protected readonly headers = signal<string[]>([...this.INITIAL_HEADERS]);

  protected handleEnableCustomer(customerId: number){
    this.onEnableCustomerEvent.emit(customerId);
  }
  protected handleDisableCustomer(customerId: number){
    this.onDisableCustomerEvent.emit(customerId);
  }
  protected handleEditCustomer(customerId: number) {
    this.onEditCustomerEvent.emit(customerId);
  }
  protected handleToNextPage() {
    this.onNextPageEvent.emit();
  }
  protected handleToPreviousPage() {
    this.onPreviousPageEvent.emit();
  }
  protected handleDisplayCustomerDetails(id: number) {
    if(this.customerTableRowElements() !== null && 
      this.customerTableRowElements().length > 0)
      {
        for (let index = 0; index < this.customerTableRowElements().length; index++) {
          const element = this.customerTableRowElements()[index];
          if(element.customer().id !== id)
            element.hideDetailsPanel();
        }
      }
  }

  public notifyEditModeEnabled() {
    if(this.customerTableRowElements() !== null && 
      this.customerTableRowElements().length > 0)
      {
        for (let index = 0; index < this.customerTableRowElements().length; index++) {
          const element = this.customerTableRowElements()[index];
          element.disableEditMode();
        }
      }
  }
  public notifyEditModeDisabled() {
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

  protected isLoading() : boolean {
    return (this.customers() && this.customers().length > 0);
  }
}