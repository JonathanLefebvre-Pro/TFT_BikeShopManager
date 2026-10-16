import { AddCustomerFormData } from '@presentation/forms/addCustomerFormData';
import { CustomerService } from '@infrastructure/services/customer-service';
import { Component, inject, output, signal } from '@angular/core';
import { email, form, maxLength, minLength, required, FormRoot } from '@angular/forms/signals';
import { CustomerAddDto } from '@appCore/dtos/customerAddDto';
import { DatePipe } from '@angular/common';
import { FormTextInput } from '@presentation/components/shared/form-text-input/form-text-input';
import { FormDateInput } from '@presentation/components/shared/form-date-input/form-date-input';
import { FormCheckboxInput } from '@presentation/components/shared/form-checkbox-input/form-checkbox-input';

@Component({
  imports: [FormTextInput, FormDateInput, FormCheckboxInput, FormRoot],
  selector: 'app-customer-add-panel',
  styleUrl: './customer-add-panel.scss',
  templateUrl: './customer-add-panel.html',
})
export class CustomerAddPanel {

  customerService = inject(CustomerService);
  onAddCustomerEvent = output<number>();

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
  customerModel = signal<AddCustomerFormData>({...this.INITIAL_MODEL});

  customerForm = form(this.customerModel, (schemaPath) => {
    required(schemaPath.firstName, {message: "Firstname is required !"}),
    minLength(schemaPath.firstName, 5, {message: "Firstname min length : 5 !"}),
    maxLength(schemaPath.firstName, 100,  {message: "Firstname max length : 100 !"}),
    required(schemaPath.lastName, {message: "LastName is required !"}),
    minLength(schemaPath.lastName, 5, {message: "LastName min length : 5 !"}),
    maxLength(schemaPath.lastName, 100,  {message: "LastName max length : 100 !"}),
    maxLength(schemaPath.vatNumber, 20, {message: "Vat number max length : 20 !"}),
    email(schemaPath.email, {message: "Enter a valid email !"}),
    maxLength(schemaPath.email, 256, { message: "Email max length : 256 !"}),
    maxLength(schemaPath.phoneNumber, 20, { message: "Phone number max length : 20 !"}),
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