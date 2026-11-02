import { AddCustomerFormData } from '@/app/presentation/forms/customers/addCustomerFormData';
import { CustomerService } from '@infrastructure/services/customer-service';
import { Component, inject, output, signal } from '@angular/core';
import { email, form, maxLength, minLength, required, FormRoot, pattern } from '@angular/forms/signals';
import { CustomerAddDto } from '@/app/appCore/dtos/customers/customerAddDto';
import { DatePipe } from '@angular/common';
import { FormTextInput } from '@presentation/components/shared/form-text-input/form-text-input';
import { FormDateInput } from '@presentation/components/shared/form-date-input/form-date-input';
import { FormCheckboxInput } from '@presentation/components/shared/form-checkbox-input/form-checkbox-input';
import { ActivatedRoute, Router } from '@angular/router';

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
    required(schemaPath.firstName, {message: "Firstname is required !"}),
    minLength(schemaPath.firstName, 5, {message: "Firstname min length : 5 !"}),
    maxLength(schemaPath.firstName, 100,  {message: "Firstname max length : 100 !"}),
    required(schemaPath.lastName, {message: "LastName is required !"}),
    minLength(schemaPath.lastName, 5, {message: "LastName min length : 5 !"}),
    maxLength(schemaPath.lastName, 100,  {message: "LastName max length : 100 !"}),
    maxLength(schemaPath.vatNumber, 15, {message: "Vat number max length : 15 !"}),
    pattern(schemaPath.email, /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*\.[a-zA-Z]{2,}$/, {message: "Enter a valid email !"}),
    maxLength(schemaPath.email, 254, { message: "Email max length : 256 !"}),
    maxLength(schemaPath.phoneNumber, 20, { message: "Phone number max length : 20 !"}),
    pattern(schemaPath.phoneNumber, /^(?:(?:\+|00)32|0)\s?(?:[1-9]\d?)(?:[\s./-]?\d{2,3}){3,4}$/, { message: "Invalid phone number !" }),
    maxLength(schemaPath.address, 200, { message: "Address max length : 200 !"}),
    maxLength(schemaPath.city, 50, { message: "City max length : 50 !"}),
    maxLength(schemaPath.zipCode, 10, { message: "ZipCode max length : 10 !"}),
    maxLength(schemaPath.country, 50, { message: "Country max length : 50 !"})
  },
  {
    submission: {
      action: async (model) => {
        const customerAddDto: CustomerAddDto = {
          vatNumber: model().value().vatNumber,
          firstName: model().value().firstName,
          lastName: model().value().lastName,
          email: (model().value().email === "") ? null : model().value().email,
          phoneNumber: (model().value().phoneNumber === "") ? null : model().value().phoneNumber,
          address: (model().value().address === "") ? null : model().value().address,
          city: (model().value().city === "") ? null : model().value().city,
          zipCode: (model().value().zipCode === "") ? null : model().value().zipCode,
          country: (model().value().country === "") ? null : model().value().country,
          birthDate: (model().value().birthDate === null) ? null : this.datepipe.transform(model().value().birthDate, 'yyyy-MM-dd'),
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