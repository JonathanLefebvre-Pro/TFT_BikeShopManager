import { Component, input } from '@angular/core';
import { User } from '../../../domain/user';
import { DatePipe } from '@angular/common';

@Component({
  imports: [DatePipe],
  selector: 'app-user-table-row',
  styleUrl: './user-table-row.scss',
  templateUrl: './user-table-row.html',
})
export class UserTableRow {
  public user = input.required<User>();
}
