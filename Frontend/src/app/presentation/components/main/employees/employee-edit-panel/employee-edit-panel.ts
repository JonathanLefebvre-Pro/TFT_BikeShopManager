import { Component, inject, input, OnInit, output, signal } from '@angular/core';
import { FormTextInput } from '@presentation/components/shared/form-text-input/form-text-input';
import { FormDateInput } from '@presentation/components/shared/form-date-input/form-date-input';
import { apply, form, FormRoot, required, validate } from '@angular/forms/signals';
import { DatePipe } from '@angular/common';
import { Employee } from '@/app/domain/employee';
import { EmployeeUpdateDto } from '@/app/appCore/dtos/employees/employeeUpdateDto';
import { UpdateEmployeeFormData } from '@/app/presentation/forms/models/employees/updateEmployeeFormData';
import { FormSelectInput } from '@presentation/components/shared/form-select-input/form-select-input';
import { addressValidationSchema, cityValidationSchema, countryValidationSchema, emailValidationSchema, firstnameValidationSchema, lastnameValidationSchema, phoneNumberValidationSchema, zipCodeValidationSchema } from '@/app/presentation/forms/validations/userValidationSchema';
import { hiredAtValidationSchema } from '@/app/presentation/forms/validations/employeeValidationSchema';
import { EmployeeFormDtoMapper } from '@/app/presentation/mappers/employees/employee-form-dto-mapper';
import { DateUtilityService } from '@/app/infrastructure/utilities/date-utility-service';

@Component({
  imports: [FormTextInput, FormDateInput, FormRoot, FormSelectInput],
  selector: 'app-employee-edit-panel',
  styleUrl: './employee-edit-panel.scss',
  templateUrl: './employee-edit-panel.html',
})
export class EmployeeEditPanel implements OnInit {

  protected readonly employeeFormDtoMapper = inject(EmployeeFormDtoMapper);
  protected readonly dateUtilityService = inject(DateUtilityService);

  protected readonly errorMessage: string = 'Something went wrong !';
  protected readonly displayErrorMessage = signal<boolean>(false);

  constructor(public datepipe: DatePipe){}
  
  public ngOnInit(): void {
    this.employeeModel.set({
      hiredAt: this.employee().hiredAt,
      contractEndAt: this.employee().contractEndAt,
      firstName: this.employee().user.firstName,
      lastName: this.employee().user.lastName,
      email: this.employee().user.email ?? '',
      phoneNumber: this.employee().user.phoneNumber ?? '',
      address: this.employee().user.address ?? '',
      city: this.employee().user.city ?? '',
      zipCode: this.employee().user.zipCode ?? '',
      country: this.employee().user.country ?? '',
      birthDate: this.employee().user.birthDate,
      allowMarketing: this.employee().user.allowMarketing,
      roleId: this.employee().role.id
    });
  }

  public employee = input.required<Employee>();
  public roleSelectOptions = input.required<string[]>();

  public onUpdateEmployeeEvent = output<{id: number, obj: EmployeeUpdateDto}>();
  public onCancelUpdateEmployeeEvent = output();

  private readonly INITIAL_MODEL = {
      hiredAt: this.dateUtilityService.getDate(),
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
        this.onUpdateEmployeeEvent.emit({
          id: this.employee().id,
          obj: this.employeeFormDtoMapper.ToEmployeeUpdateDto(model().value())
        });
      }
    }
  });

  protected handleCancel(event: Event) {
    event.preventDefault();
    this.onCancelUpdateEmployeeEvent.emit();
  }

  public handleDisplayErrorMessage() {
    this.displayErrorMessage.set(true);
  }

  public handleFormReset() {
    this.employeeForm().reset({...this.INITIAL_MODEL}); 
  }
}
