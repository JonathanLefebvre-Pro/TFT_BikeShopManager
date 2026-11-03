import { AddCustomerFormData } from '@/app/presentation/forms/models/customers/addCustomerFormData';
import { CustomerService } from '@infrastructure/services/customer-service';
import { Component, DestroyRef, inject, output, signal } from '@angular/core';
import { form, FormRoot, apply } from '@angular/forms/signals';
import { DatePipe } from '@angular/common';
import { FormTextInput } from '@presentation/components/shared/form-text-input/form-text-input';
import { FormDateInput } from '@presentation/components/shared/form-date-input/form-date-input';
import { FormCheckboxInput } from '@presentation/components/shared/form-checkbox-input/form-checkbox-input';
import { ActivatedRoute, Router } from '@angular/router';
import { vatNumberValidationSchema } from '@/app/presentation/forms/validations/customerValidationSchema';
import { addressValidationSchema, cityValidationSchema, countryValidationSchema, emailValidationSchema, firstnameValidationSchema, lastnameValidationSchema, phoneNumberValidationSchema, zipCodeValidationSchema } from '@/app/presentation/forms/validations/userValidationSchema';
import { CustomerFormDtoMapper } from '@/app/presentation/mappers/customers/customer-form-dto-mapper';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

@Component({
  imports: [FormTextInput, FormDateInput, FormCheckboxInput, FormRoot],
  selector: 'app-customer-add-panel',
  styleUrl: './customer-add-panel.scss',
  templateUrl: './customer-add-panel.html',
})
export class CustomerAddPanel {

  protected readonly destroyRef = inject(DestroyRef);
  protected readonly customerService = inject(CustomerService);
  protected readonly customerFormDtoMapper = inject(CustomerFormDtoMapper);
  protected readonly routerService = inject(Router);
  protected readonly routeService = inject(ActivatedRoute);

  public onAddCustomerEvent = output<number>();

  constructor(public datepipe: DatePipe){}

  private readonly INITIAL_MODEL = {
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
      allowMarketing: true
  };
  protected customerModel = signal<AddCustomerFormData>({...this.INITIAL_MODEL});

  protected customerForm = form(this.customerModel, (schemaPath) => {
    //Validation : Firstname (Required)
    apply(schemaPath.firstName, firstnameValidationSchema),
    //Validation : Lastname (Required)
    apply(schemaPath.lastName, lastnameValidationSchema),
    //Validation : VAT Number
    apply(schemaPath.vatNumber, vatNumberValidationSchema),
    //Validation : Email
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
    apply(schemaPath.country, countryValidationSchema)
  },
  {
    submission: {
      action: async (model) => {
        await this.customerService.addCustomer(
          this.customerFormDtoMapper.ToCustomerAddDto(model().value()))
          .pipe(takeUntilDestroyed(this.destroyRef))
          .subscribe({
            next: (res) => {
              model().reset({...this.INITIAL_MODEL});
              this.onAddCustomerEvent.emit(res);
              this.routerService.navigate(['..', 'list'], { relativeTo: this.routeService });
            },
            error: (error) => {
              console.error('Error : ', error);
            }
        });
      }
    }
  });
}