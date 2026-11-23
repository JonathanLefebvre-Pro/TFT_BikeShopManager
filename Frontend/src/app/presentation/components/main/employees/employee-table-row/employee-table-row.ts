import { Employee } from '@/app/domain/employee';
import { DatePipe } from '@angular/common';
import { Component, input, OnChanges, output, signal, SimpleChanges } from '@angular/core';
import { EmptyStringPlaceholderPipe } from '@/app/appCore/pipes/empty-string-placeholder-pipe';
import { NgIcon, provideIcons } from '@ng-icons/core';
import { matAccountCircle, matEdit, matNoAccounts } from '@ng-icons/material-icons/baseline';

@Component({
  imports: [DatePipe, EmptyStringPlaceholderPipe, NgIcon],
  selector: 'app-employee-table-row',
  styleUrl: './employee-table-row.scss',
  templateUrl: './employee-table-row.html',
  providers: [provideIcons({matNoAccounts, matAccountCircle, matEdit})]
})
export class EmployeeTableRow implements OnChanges {
  
  public employee = input.required<Employee>();
  public readonly disableActivationButton = input<boolean>(false);
  protected isEditModeEnabled = signal<boolean>(false);
  protected displayDetails = signal<boolean>(false);

  public onEnableEmployeeEvent = output<number>();
  public onDisableEmployeeEvent = output<number>();
  public onEditEmployeeEvent = output<number>();
  public onDisplayDetailsEmployeeEvent = output<number>();

  public ngOnChanges(changes: SimpleChanges): void {
    this.isEditModeEnabled.set(false);
  }

  protected handleEnableEmployee(event: Event) {
    event.preventDefault();
    event.stopPropagation();
    this.onEnableEmployeeEvent.emit(this.employee().id);
  }

  protected handleDisableEmployee(event: Event) {
    event.preventDefault();
    event.stopPropagation();
    this.onDisableEmployeeEvent.emit(this.employee().id);
  }

  protected handleEditEmployee(event: Event) {
    event.preventDefault();
    event.stopPropagation();
    this.isEditModeEnabled.set(true);
    this.onEditEmployeeEvent.emit(this.employee().id);
  }

  protected handleDisplayDetails() {
    this.onDisplayDetailsEmployeeEvent.emit(this.employee().id);
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