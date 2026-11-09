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
  public readonly authUser = input<Employee | null>();
  public readonly currentPageNumber = input.required<number>();
  public readonly maxPagesNumber = input.required<number>();

  public onEnableEmployeeEvent = output<number>();
  public onDisableEmployeeEvent = output<number>();
  public onEditEmployeeEvent = output<number>();
  public onNextPageEvent = output();
  public onPreviousPageEvent = output();

  protected readonly employeeTableRowElements = viewChildren(EmployeeTableRow);

  private readonly INITIAL_HEADERS : string[] = [
    '#',
    'FirstName', 
    'LastName', 
    'Email', 
    'IsActive', 
    'Role', 
    'Actions'
  ];
  protected readonly headers = signal<string[]>([...this.INITIAL_HEADERS]);

  protected handleEnableEmployee(employeeId: number){
    this.onEnableEmployeeEvent.emit(employeeId);
  }
  protected handleDisableEmployee(employeeId: number){
    this.onDisableEmployeeEvent.emit(employeeId);
  }
  protected handleEditEmployee(employeeId: number) {
    this.onEditEmployeeEvent.emit(employeeId);
  }
  protected handleToNextPage() {
    this.onNextPageEvent.emit();
  }
  protected handleToPreviousPage() {
    this.onPreviousPageEvent.emit();
  }
  protected handleDisplayEmployeeDetails(id: number) {
    if(this.employeeTableRowElements() !== null && 
      this.employeeTableRowElements().length > 0)
      {
        for (let index = 0; index < this.employeeTableRowElements().length; index++) {
          const element = this.employeeTableRowElements()[index];
          if(element.employee().id !== id)
            element.hideDetailsPanel();
        }
      }
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
