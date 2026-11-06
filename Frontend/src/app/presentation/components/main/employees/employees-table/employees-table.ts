import { Component, input, output, signal, viewChildren } from '@angular/core';
import { EmployeeTableRow } from '../employee-table-row/employee-table-row';
import { Employee } from '@/app/domain/employee';

@Component({
  imports: [EmployeeTableRow],
  selector: 'app-employees-table',
  styleUrl: './employees-table.scss',
  templateUrl: './employees-table.html',
})
export class EmployeesTable {
  public readonly employees = input.required<Employee[]>();
  public readonly currentPageNumber = input.required<number>();
  public readonly maxPagesNumber = input.required<number>();

  public onEnableEmployeeEvent = output<number>();
  public onDisableEmployeeEvent = output<number>();
  public onEditEmployeeEvent = output<number>();
  public onNextPageEvent = output();
  public onPreviousPageEvent = output();

  protected readonly employeeTableRowElements = viewChildren(EmployeeTableRow);

  private readonly INITIAL_HEADERS : string[] = ['#','FirstName', 'LastName', 'Email', 'PhoneNumber', 
    'Address', 'City', 'ZipCode', 'Country', 'BirthDate', 'IsActive', 'HiredAt', 'ContractEndAt', 'Role', 'Actions'];
  protected readonly headers = signal<string[]>([...this.INITIAL_HEADERS]);

  protected handleEmployeeEnable(employeeId: number){
    this.onEnableEmployeeEvent.emit(employeeId);
  }
  protected handleEmployeeDisable(employeeId: number){
    this.onDisableEmployeeEvent.emit(employeeId);
  }
  protected handleEmployeeEdit(employeeId: number) {
    this.onEditEmployeeEvent.emit(employeeId);
  }
  protected handleNextPage() {
    this.onNextPageEvent.emit();
  }
  protected handlePreviousPage() {
    this.onPreviousPageEvent.emit();
  }

  public notifyEditModeEnabled() {
    if(this.employeeTableRowElements() !== null && 
      this.employeeTableRowElements().length > 0)
      {
        for (let index = 0; index < this.employeeTableRowElements().length; index++) {
          const element = this.employeeTableRowElements()[index];
          element.disableEditMode();
        }
      }
  }
  public notifyEditModeDisabled() {
    if(this.employeeTableRowElements() !== null && 
      this.employeeTableRowElements().length > 0)
      {
        for (let index = 0; index < this.employeeTableRowElements().length; index++) {
          const element = this.employeeTableRowElements()[index];
          if(element.employee().isActive)
            element.enableEditMode();
        }
      }
  }

  protected isLoading() : boolean {
    return (this.employees() && this.employees().length > 0);
  }
}
