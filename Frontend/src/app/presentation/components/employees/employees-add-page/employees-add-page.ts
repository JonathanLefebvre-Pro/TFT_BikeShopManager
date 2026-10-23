import { Component } from '@angular/core';
import { EmployeeAddPanel } from '../employee-add-panel/employee-add-panel';

@Component({
  imports: [EmployeeAddPanel],
  selector: 'app-employees-add-page',
  styleUrl: './employees-add-page.scss',
  templateUrl: './employees-add-page.html',
})
export class EmployeesAddPage {}
