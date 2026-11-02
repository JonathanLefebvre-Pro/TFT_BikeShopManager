import { Component, input, OnInit, output, signal } from '@angular/core';
import { FormTextInput } from '@presentation/components/shared/form-text-input/form-text-input';
import { FormDateInput } from '@presentation/components/shared/form-date-input/form-date-input';
import { FormCheckboxInput } from '@presentation/components/shared/form-checkbox-input/form-checkbox-input';
import { apply, email, form, FormRoot, maxLength, minLength, pattern, required } from '@angular/forms/signals';
import { DatePipe } from '@angular/common';
import { Employee } from '@/app/domain/employee';
import { EmployeeUpdateDto } from '@/app/appCore/dtos/employees/employeeUpdateDto';
import { UpdateEmployeeFormData } from '@/app/presentation/forms/employees/updateEmployeeFormData';
import { FormSelectInput } from '@presentation/components/shared/form-select-input/form-select-input';
import { addressValidationSchema, cityValidationSchema, countryValidationSchema, emailValidationSchema, firstnameValidationSchema, lastnameValidationSchema, phoneNumberValidationSchema, zipCodeValidationSchema } from '@/app/presentation/forms/validations/userValidationSchema';

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

    //TODO : Add input for password update ???
    //TODO : Add validation with password best practices (NIST 800-63B) and with double input for password confirmation

    //Validation : HiredAt
    //TODO : Add validation for hiredAt and contractEndAt (contractEndAt must be after hiredAt)
    required(schemaPath.hiredAt, { message: "Hired date is required !" }),
    //Validation : Firstname
    apply(schemaPath.firstName, firstnameValidationSchema),
    //Validation : Lastname
    apply(schemaPath.lastName, lastnameValidationSchema),
    //Validation : Email
    required(schemaPath.email, { message: "Email is required !" }),
    apply(schemaPath.email, emailValidationSchema),
    //Validation : Phone Number
    apply(schemaPath.phoneNumber, phoneNumberValidationSchema),
    //Validation : Address
    apply(schemaPath.address, addressValidationSchema),
    //Validation : City
    apply(schemaPath.city, cityValidationSchema),
    //Validation : ZipCode
    apply(schemaPath.zipCode, zipCodeValidationSchema),
    //Validation : Country
    apply(schemaPath.country, countryValidationSchema),
    //Validation : RoleId
    required(schemaPath.roleId, { message: "Role id is required !" })
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
