import { Employee } from '@/app/domain/employee';
import { DatePipe } from '@angular/common';
import { Component, input, OnChanges, OnInit, output, signal, SimpleChanges } from '@angular/core';
import { ListFieldEmptyPlaceholderPipe } from '@appCore/pipes/list-field-empty-placeholder-pipe';

@Component({
  imports: [DatePipe, ListFieldEmptyPlaceholderPipe],
  selector: 'app-employee-table-row',
  styleUrl: './employee-table-row.scss',
  templateUrl: './employee-table-row.html',
})
export class EmployeeTableRow implements OnChanges {
  
  public employee = input.required<Employee>();
  protected isEditModeEnabled = signal<boolean>(false);
  
  public onEnableEmployeeEvent = output<number>();
  public onDisableEmployeeEvent = output<number>();
  public onEditEmployeeEvent = output<number>();
  
  public ngOnChanges(changes: SimpleChanges): void {
    this.isEditModeEnabled.set(false);
  }

  protected handleEnableEmployee() {
    this.onEnableEmployeeEvent.emit(this.employee().id);
  }

  protected handleDisableEmployee() {
    this.onDisableEmployeeEvent.emit(this.employee().id);
  }

  protected handleEditEmployee() {
    this.isEditModeEnabled.set(true);
    this.onEditEmployeeEvent.emit(this.employee().id);
  }

  public disableEditMode() {
    this.isEditModeEnabled.set(true);
  }
  public enableEditMode() {
    this.isEditModeEnabled.set(false);
  }
}