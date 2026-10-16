import { AddCustomerFormData } from '@/app/presentation/forms/addCustomerFormData';
import { CustomerService } from '@/app/infrastructure/services/customer-service';
import { Component, inject, signal } from '@angular/core';
import { email, form, maxLength, minLength, required, submit } from '@angular/forms/signals';
import { CustomerAddDto } from '@/app/appCore/dtos/customerAddDto';
import { DatePipe } from '@angular/common';
import { FormTextInput } from '../../shared/form-text-input/form-text-input';
import { FormDateInput } from '../../shared/form-date-input/form-date-input';
import { FormCheckboxInput } from '../../shared/form-checkbox-input/form-checkbox-input';

@Component({
  imports: [FormTextInput, FormDateInput, FormCheckboxInput],
  selector: 'app-add-customer',
  styleUrl: './add-customer.scss',
  templateUrl: './add-customer.html',
})
export class AddCustomer {

  customerService = inject(CustomerService);
  customerModel = signal<AddCustomerFormData>({
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
  });

  customerForm = form(this.customerModel, (fieldPath) => {
    required(fieldPath.firstName, {message: "Firstname is required !"}),
    minLength(fieldPath.firstName, 5, {message: "Firstname min length : 5 !"}),
    maxLength(fieldPath.firstName, 100,  {message: "Firstname max length : 100 !"}),
    required(fieldPath.lastName, {message: "LastName is required !"}),
    minLength(fieldPath.lastName, 5, {message: "LastName min length : 5 !"}),
    maxLength(fieldPath.lastName, 100,  {message: "LastName max length : 100 !"}),
    maxLength(fieldPath.vatNumber, 20, {message: "Vat number max length : 20 !"}),
    email(fieldPath.email, {message: "Enter a valid email !"}),
    maxLength(fieldPath.email, 256, { message: "Email max length : 256 !"}),
    maxLength(fieldPath.phoneNumber, 20, { message: "Phone number max length : 20 !"}),
    maxLength(fieldPath.address, 200, { message: "Address max length : 200 !"}),
    maxLength(fieldPath.city, 50, { message: "City max length : 50 !"}),
    maxLength(fieldPath.zipCode, 10, { message: "ZipCode max length : 10 !"}),
    maxLength(fieldPath.country, 50, { message: "Country max length : 50 !"})
  });

  constructor(public datepipe: DatePipe){}

  handleSubmit(event: Event) {
    event.preventDefault();

    submit(this.customerForm, async () => {

      const model = this.customerModel();

      const customerAddDto: CustomerAddDto = {
        vatNumber: model.vatNumber,
        firstName: model.firstName,
        lastName: model.lastName,
        email: (model.email === "") ? null : model.email,
        phoneNumber: (model.phoneNumber === "") ? null : model.phoneNumber,
        address: (model.address === "") ? null : model.address,
        city: (model.city === "") ? null : model.city,
        zipCode: (model.zipCode === "") ? null : model.zipCode,
        country: (model.country === "") ? null : model.country,
        birthDate: (model.birthDate === null) ? null : this.datepipe.transform(model.birthDate, 'yyyy-MM-dd'),
        allowMarketing: model.allowMarketing
      }

      this.customerService.addCustomer(customerAddDto).subscribe({
        next: (res) => {
          console.log("Result : " + JSON.stringify(res));
          // JSON.parse()
        },
        error: (error) => {
          console.log("Error : " + JSON.stringify(error));
          console.log(JSON.stringify(customerAddDto));
        }
      });
    });
  }
}
