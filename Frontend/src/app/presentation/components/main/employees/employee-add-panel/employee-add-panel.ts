import { Component, inject, input, output, signal } from '@angular/core';
import { FormTextInput } from '@presentation/components/shared/form-text-input/form-text-input';
import { FormDateInput } from '@presentation/components/shared/form-date-input/form-date-input';
import { FormCheckboxInput } from '@presentation/components/shared/form-checkbox-input/form-checkbox-input';
import { apply, email, form, FormRoot, maxLength, minLength, required } from '@angular/forms/signals';
import { EmployeeService } from '@/app/infrastructure/services/employee-service';
import { ActivatedRoute, Router } from '@angular/router';
import { DatePipe } from '@angular/common';
import { AddEmployeeFormData } from '@/app/presentation/forms/employees/addEmployeeFormData';
import { EmployeeAddDto } from '@/app/appCore/dtos/employees/employeeAddDto';
import { FormSelectInput } from '@presentation/components/shared/form-select-input/form-select-input';
import { passwordValidationSchema } from '@/app/presentation/forms/validations/loginValidationSchema';
import { addressValidationSchema, cityValidationSchema, countryValidationSchema, emailValidationSchema, firstnameValidationSchema, lastnameValidationSchema, phoneNumberValidationSchema, zipCodeValidationSchema } from '@/app/presentation/forms/validations/userValidationSchema';

@Component({
  imports: [FormTextInput, FormDateInput, FormCheckboxInput, FormRoot, FormSelectInput],
  selector: 'app-employee-add-panel',
  styleUrl: './employee-add-panel.scss',
  templateUrl: './employee-add-panel.html',
})
export class EmployeeAddPanel {

  protected readonly employeeService = inject(EmployeeService);
  protected readonly routerService = inject(Router);
  protected readonly routeService = inject(ActivatedRoute);

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
    //Validation : HiredAt
    //TODO : Add validation for hiredAt and contractEndAt (contractEndAt must be after hiredAt)
    required(schemaPath.hiredAt, { message: "Hired date is required !" }),
    //Validation : Password
    //TODO : Add validation with password best practices (NIST 800-63B) and with double input for password confirmation
    apply(schemaPath.password, passwordValidationSchema),
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
    apply(schemaPath.zipCode, zipCodeValidationSchema)
    //Validation : Country
    apply(schemaPath.country, countryValidationSchema)
    //Validation : RoleId
    required(schemaPath.roleId, { message: "Role id is required !" })
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
            this.routerService.navigate(['..', 'list'], { relativeTo: this.routeService });
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
