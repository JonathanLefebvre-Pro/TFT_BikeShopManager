import { Component, input } from '@angular/core';
import { User } from '../../domain/user';
import { DatePipe } from '@angular/common';

@Component({
  imports: [DatePipe],
  selector: 'app-user-form-row',
  styleUrl: './user-form-row.scss',
  templateUrl: './user-form-row.html',
})
export class UserFormRow {
  public user = input.required<User>();
}
