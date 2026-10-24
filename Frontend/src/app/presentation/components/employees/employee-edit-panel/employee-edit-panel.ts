import { Component, input, OnInit, output, signal } from '@angular/core';
import { FormTextInput } from '../../shared/form-text-input/form-text-input';
import { FormDateInput } from '../../shared/form-date-input/form-date-input';
import { FormCheckboxInput } from '../../shared/form-checkbox-input/form-checkbox-input';
import { email, form, FormRoot, maxLength, minLength, required } from '@angular/forms/signals';
import { DatePipe } from '@angular/common';
import { Employee } from '@/app/domain/employee';
import { EmployeeUpdateDto } from '@/app/appCore/dtos/employees/employeeUpdateDto';
import { UpdateEmployeeFormData } from '@/app/presentation/forms/employees/updateEmployeeFormData';
import { FormSelectInput } from '../../shared/form-select-input/form-select-input';

@Component({
  imports: [FormTextInput, FormDateInput, FormCheckboxInput, FormRoot, FormSelectInput],
  selector: 'app-employee-edit-panel',
  styleUrl: './employee-edit-panel.scss',
  templateUrl: './employee-edit-panel.html',
})
export class EmployeeEditPanel implements OnInit {
  constructor(public datepipe: DatePipe){}
  
  public ngOnInit(): void {
    this.employeeModel.set({
      hiredAt: this.employee().hiredAt ?? '',
      contractEndAt: this.employee().contractEndAt ?? null,
      firstName: this.employee().user.firstName,
      lastName: this.employee().user.lastName,
      email: this.employee().user.email ?? '',
      phoneNumber: this.employee().user.phoneNumber ?? '',
      address: this.employee().user.address ?? '',
      city: this.employee().user.city ?? '',
      zipCode: this.employee().user.zipCode ?? '',
      country: this.employee().user.country ?? '',
      birthDate: this.employee().user.birthDate ?? null,
      allowMarketing: this.employee().user.allowMarketing,
      roleId: this.employee().role.id
    });
  }

  public employee = input.required<Employee>();
  public roleSelectOptions = input.required<string[]>();

  public onUpdateEmployeeEvent = output<{id: number, obj: EmployeeUpdateDto}>();
  public onCancelUpdateEmployeeEvent = output();

  private readonly INITIAL_MODEL = {
      hiredAt: new Date('2023-02-01'),
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
  protected employeeModel = signal<UpdateEmployeeFormData>({...this.INITIAL_MODEL});

  protected employeeForm = form(this.employeeModel, (schemaPath) => {
    required(schemaPath.hiredAt, {message: "Hired date is required !"}),
    required(schemaPath.firstName, {message: "Firstname is required !"}),
    minLength(schemaPath.firstName, 5, {message: "Firstname min length : 5 !"}),
    maxLength(schemaPath.firstName, 100,  {message: "Firstname max length : 100 !"}),
    required(schemaPath.lastName, {message: "LastName is required !"}),
    minLength(schemaPath.lastName, 5, {message: "LastName min length : 5 !"}),
    maxLength(schemaPath.lastName, 100,  {message: "LastName max length : 100 !"}),
    email(schemaPath.email, {message: "Enter a valid email !"}),
    required(schemaPath.email, {message: "Email is required !"}),
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
        const employeeUpdateDto: EmployeeUpdateDto = {
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

        this.onUpdateEmployeeEvent.emit({id: this.employee().id, obj: employeeUpdateDto});
        model().reset({...this.INITIAL_MODEL});
      }
    }
  });

  protected handleCancel(event: Event) {
    event.preventDefault();

    this.onCancelUpdateEmployeeEvent.emit();
  }
}
