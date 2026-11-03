import { AddCustomerFormData } from '@/app/presentation/forms/models/customers/addCustomerFormData';
import { CustomerService } from '@infrastructure/services/customer-service';
import { Component, inject, output, signal } from '@angular/core';
import { form, FormRoot, apply } from '@angular/forms/signals';
import { CustomerAddDto } from '@/app/appCore/dtos/customers/customerAddDto';
import { DatePipe } from '@angular/common';
import { FormTextInput } from '@presentation/components/shared/form-text-input/form-text-input';
import { FormDateInput } from '@presentation/components/shared/form-date-input/form-date-input';
import { FormCheckboxInput } from '@presentation/components/shared/form-checkbox-input/form-checkbox-input';
import { ActivatedRoute, Router } from '@angular/router';
import { vatNumberValidationSchema } from '@/app/presentation/forms/validations/customerValidationSchema';
import { addressValidationSchema, cityValidationSchema, countryValidationSchema, emailValidationSchema, firstnameValidationSchema, lastnameValidationSchema, phoneNumberValidationSchema, zipCodeValidationSchema } from '@/app/presentation/forms/validations/userValidationSchema';

@Component({
  imports: [FormTextInput, FormDateInput, FormCheckboxInput, FormRoot],
  selector: 'app-customer-add-panel',
  styleUrl: './customer-add-panel.scss',
  templateUrl: './customer-add-panel.html',
})
export class CustomerAddPanel {

  protected readonly customerService = inject(CustomerService);
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
        const customerAddDto: CustomerAddDto = {
          vatNumber: (model().value().vatNumber === "") ? null : model().value().vatNumber,
          firstName: model().value().firstName,
          lastName: model().value().lastName,
          email: (model().value().email === "") ? null : model().value().email,
          phoneNumber: (model().value().phoneNumber === "") ? null : model().value().phoneNumber,
          address: (model().value().address === "") ? null : model().value().address,
          city: (model().value().city === "") ? null : model().value().city,
          zipCode: (model().value().zipCode === "") ? null : model().value().zipCode,
          country: (model().value().country === "") ? null : model().value().country,
          birthDate: model().value().birthDate?.toISOString().substring(0,10) ?? null,
          allowMarketing: model().value().allowMarketing
        }

        await this.customerService.addCustomer(customerAddDto).subscribe({
          next: (res) => {
            console.log("Result : " + JSON.stringify(res));
            model().reset({...this.INITIAL_MODEL});
            this.onAddCustomerEvent.emit(res);
            this.routerService.navigate(['..', 'list'], { relativeTo: this.routeService });
          },
          error: (error) => {
            console.log("Error : " + JSON.stringify(error));
            console.log(JSON.stringify(customerAddDto));
          }
        });
      }
    }
  });
}