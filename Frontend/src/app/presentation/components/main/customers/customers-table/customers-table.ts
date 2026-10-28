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

  protected readonly customerTableRowElements = viewChildren(CustomerTableRow);

  private readonly INITIAL_HEADERS : string[] = ['#','FirstName', 'LastName', 'Email', 'PhoneNumber', 
    'Address', 'City', 'ZipCode', 'Country', 'BirthDate', 'AllowMarketing', 'IsActive', 'VatNumber', 'Actions'];
  protected readonly headers = signal<string[]>([...this.INITIAL_HEADERS]);

  protected handleCustomerEnable(customerId: number){
    this.onEnableCustomerEvent.emit(customerId);
  }
  protected handleCustomerDisable(customerId: number){
    this.onDisableCustomerEvent.emit(customerId);
  }
  protected handleCustomerEdit(customerId: number) {
    this.onEditCustomerEvent.emit(customerId);
  }
  protected handleNextPage() {
    this.onNextPageEvent.emit();
  }
  protected handlePreviousPage() {
    this.onPreviousPageEvent.emit();
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