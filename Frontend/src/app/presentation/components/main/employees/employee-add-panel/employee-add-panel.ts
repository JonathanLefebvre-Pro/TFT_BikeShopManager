import { Component, inject, input, output, signal } from '@angular/core';
import { FormTextInput } from '@presentation/components/shared/form-text-input/form-text-input';
import { FormDateInput } from '@presentation/components/shared/form-date-input/form-date-input';
import { FormCheckboxInput } from '@presentation/components/shared/form-checkbox-input/form-checkbox-input';
import { email, form, FormRoot, maxLength, minLength, required } from '@angular/forms/signals';
import { EmployeeService } from '@/app/infrastructure/services/employee-service';
import { Router } from '@angular/router';
import { DatePipe } from '@angular/common';
import { AddEmployeeFormData } from '@/app/presentation/forms/employees/addEmployeeFormData';
import { EmployeeAddDto } from '@/app/appCore/dtos/employees/employeeAddDto';
import { FormSelectInput } from '@presentation/components/shared/form-select-input/form-select-input';

@Component({
  imports: [FormTextInput, FormDateInput, FormCheckboxInput, FormRoot, FormSelectInput],
  selector: 'app-employee-add-panel',
  styleUrl: './employee-add-panel.scss',
  templateUrl: './employee-add-panel.html',
})
export class EmployeeAddPanel {

  protected employeeService = inject(EmployeeService);
  protected routerService = inject(Router);

  public onAddEmployeeEvent = output<number>();

  constructor(public datepipe: DatePipe){}
  public roleSelectOptions = input.required<string[]>();

  private readonly INITIAL_MODEL = {
      password: '',
      hiredAt: new Date('2025-04-02'),
      contractEndAt: null,
      firstName: '',
      lastName: '',
      vatNumber: '',
      email: '',
      phoneNumber: '',
      address: '',
      city: '',
      zipCode: '',
      country: '',
      birthDate: null,
      allowMarketing: true,
      roleId: 1
  };
  protected employeeModel = signal<AddEmployeeFormData>({...this.INITIAL_MODEL});

  protected employeeForm = form(this.employeeModel, (schemaPath) => {
    required(schemaPath.hiredAt, {message: "Hired date is required !"}),
    required(schemaPath.password, {message: "Password is required !"}),
    minLength(schemaPath.password, 5, {message: "Password min length : 5 !"}),
    maxLength(schemaPath.password, 100,  {message: "Password max length : 100 !"}),
    required(schemaPath.firstName, {message: "Firstname is required !"}),
    minLength(schemaPath.firstName, 5, {message: "Firstname min length : 5 !"}),
    maxLength(schemaPath.firstName, 100,  {message: "Firstname max length : 100 !"}),
    required(schemaPath.lastName, {message: "LastName is required !"}),
    minLength(schemaPath.lastName, 5, {message: "LastName min length : 5 !"}),
    maxLength(schemaPath.lastName, 100,  {message: "LastName max length : 100 !"}),
    required(schemaPath.email, {message: "Email is required !"}),
    email(schemaPath.email, {message: "Enter a valid email !"}),
    maxLength(schemaPath.email, 256, { message: "Email max length : 256 !"}),
    maxLength(schemaPath.phoneNumber, 20, { message: "Phone number max length : 20 !"}),
    maxLength(schemaPath.address, 200, { message: "Address max length : 200 !"}),
    maxLength(schemaPath.city, 50, { message: "City max length : 50 !"}),
    maxLength(schemaPath.zipCode, 10, { message: "ZipCode max length : 10 !"}),
    maxLength(schemaPath.country, 50, { message: "Country max length : 50 !"}),
    required(schemaPath.roleId, {message: "Role id is required !"})
  },
  {
    submission: {
      action: async (model) => {
        const employeeAddDto: EmployeeAddDto = {
          password: model().value().password,
          hiredAt: this.datepipe.transform(model().value().hiredAt, 'yyyy-MM-dd') ?? '',
          contractEndAt: (model().value().contractEndAt === null) ? null : this.datepipe.transform(model().value().contractEndAt, 'yyyy-MM-dd'),
          firstName: model().value().firstName,
          lastName: model().value().lastName,
          email: (model().value().email === "") ? null : model().value().email,
          phoneNumber: (model().value().phoneNumber === "") ? null : model().value().phoneNumber,
          address: (model().value().address === "") ? null : model().value().address,
          city: (model().value().city === "") ? null : model().value().city,
          zipCode: (model().value().zipCode === "") ? null : model().value().zipCode,
          country: (model().value().country === "") ? null : model().value().country,
          birthDate: (model().value().birthDate === null) ? null : this.datepipe.transform(model().value().birthDate, 'yyyy-MM-dd'),
          allowMarketing: model().value().allowMarketing,
          roleId: model().value().roleId
        }

        await this.employeeService.addEmployee(employeeAddDto).subscribe({
          next: (res) => {
            console.log("Result : " + JSON.stringify(res));
            model().reset({...this.INITIAL_MODEL});
            this.onAddEmployeeEvent.emit(res);
            this.routerService.navigate(['/employees/list']);
          },
          error: (error) => {
            console.log("Error : " + JSON.stringify(error));
            console.log(JSON.stringify(employeeAddDto));
          }
        });
      }
    }
  });
}
