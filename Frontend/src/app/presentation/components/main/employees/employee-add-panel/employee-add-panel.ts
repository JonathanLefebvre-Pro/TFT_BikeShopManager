import { Component, DestroyRef, inject, input, output, signal } from '@angular/core';
import { FormTextInput } from '@presentation/components/shared/form-text-input/form-text-input';
import { FormDateInput } from '@presentation/components/shared/form-date-input/form-date-input';
import { apply, form, FormRoot, required, validate } from '@angular/forms/signals';
import { EmployeeService } from '@/app/infrastructure/services/employee-service';
import { ActivatedRoute, Router } from '@angular/router';
import { DatePipe } from '@angular/common';
import { AddEmployeeFormData } from '@/app/presentation/forms/models/employees/addEmployeeFormData';
import { FormSelectInput } from '@presentation/components/shared/form-select-input/form-select-input';
import { passwordValidationSchema } from '@/app/presentation/forms/validations/loginValidationSchema';
import { addressValidationSchema, cityValidationSchema, countryValidationSchema, emailValidationSchema, firstnameValidationSchema, lastnameValidationSchema, phoneNumberValidationSchema, zipCodeValidationSchema } from '@/app/presentation/forms/validations/userValidationSchema';
import { hiredAtValidationSchema } from '@/app/presentation/forms/validations/employeeValidationSchema';
import { EmployeeFormDtoMapper } from '@/app/presentation/mappers/employees/employee-form-dto-mapper';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

@Component({
  imports: [FormTextInput, FormDateInput, FormRoot, FormSelectInput],
  selector: 'app-employee-add-panel',
  styleUrl: './employee-add-panel.scss',
  templateUrl: './employee-add-panel.html',
})
export class EmployeeAddPanel {

  protected readonly destroyRef = inject(DestroyRef);
  protected readonly employeeService = inject(EmployeeService);
  protected readonly employeeFormDtoMapper = inject(EmployeeFormDtoMapper);
  protected readonly routerService = inject(Router);
  protected readonly routeService = inject(ActivatedRoute);

  public onAddEmployeeEvent = output<number>();
  public roleSelectOptions = input.required<string[]>();

  protected errorMessage: string = 'Something went wrong !';
  protected readonly displayErrorMessage = signal<boolean>(false);

  constructor(public datepipe: DatePipe){}

  private getDate() : Date {
    var date = new Date();
    date.setUTCHours(0, 0, 0, 0);
    return date;
  }

  private readonly INITIAL_MODEL = {
      password: '',
      passwordConfirm: '',
      hiredAt: this.getDate(),
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
    apply(schemaPath.hiredAt, hiredAtValidationSchema),
    //Validation : ContractEndAt
    validate(schemaPath.contractEndAt, ({value, valueOf}) => {  
      const contractEndAt = value();
      const hiredAt = valueOf(schemaPath.hiredAt);

      if(!contractEndAt) return undefined;
      if(isNaN(contractEndAt.valueOf())) return undefined;

      if(!hiredAt) return undefined;
      if(isNaN(hiredAt.valueOf())) return undefined;

      if(contractEndAt.getTime() <= hiredAt.getTime())
        return { kind: "min-date", message: "Contract end date must be after hired date !" };
      else return undefined;
    }),
    //Validation : Password
    //TODO : Add validation with password best practices (NIST 800-63B) 
    apply(schemaPath.password, passwordValidationSchema),
    validate(schemaPath.passwordConfirm, ({value, valueOf}) => {
        const password: string = valueOf(schemaPath.password);
        const passwordConfirm: string = value();

        if(!password) return undefined;
        if(!passwordConfirm) return undefined;

        if(password !== passwordConfirm)
          return { kind: 'password-confirm', message: "The password does not match !" }

        return null;
    }),
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
        await this.employeeService.addEmployee(
          this.employeeFormDtoMapper.ToEmployeeAddDto(model().value()))
          .pipe(takeUntilDestroyed(this.destroyRef))
          .subscribe({
            next: (res) => {
              model().reset({...this.INITIAL_MODEL});
              this.onAddEmployeeEvent.emit(res);
              this.routerService.navigate(['..', 'list'], { relativeTo: this.routeService });
            },
            error: (error) => {
              console.log('Error : ', error);
              this.displayErrorMessage.set(true);
            }
        });
      }
    }
  });
}
