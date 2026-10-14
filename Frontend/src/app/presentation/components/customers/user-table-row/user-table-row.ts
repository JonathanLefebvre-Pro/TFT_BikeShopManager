import { Component, input } from '@angular/core';
import { DatePipe } from '@angular/common';
import { Customer } from '@/app/domain/customer';

@Component({
  imports: [DatePipe],
  selector: 'app-user-table-row',
  styleUrl: './user-table-row.scss',
  templateUrl: './user-table-row.html',
})
export class UserTableRow {
  public customer = input.required<Customer>();
}
