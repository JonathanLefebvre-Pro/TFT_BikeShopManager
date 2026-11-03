import { Customer } from '@domain/customer';
import { Component, input, OnInit, output, signal } from '@angular/core';
import { FormTextInput } from '@presentation/components/shared/form-text-input/form-text-input';
import { FormDateInput } from '@presentation/components/shared/form-date-input/form-date-input';
import { FormCheckboxInput } from '@presentation/components/shared/form-checkbox-input/form-checkbox-input';
import { UpdateCustomerFormData } from '@/app/presentation/forms/models/customers/updateCustomerFormData';
import { form, FormRoot, apply } from '@angular/forms/signals';
import { CustomerUpdateDto } from '@/app/appCore/dtos/customers/customerUpdateDto';
import { DatePipe } from '@angular/common';
import { vatNumberValidationSchema } from '@/app/presentation/forms/validations/customerValidationSchema';
import { addressValidationSchema, cityValidationSchema, countryValidationSchema, emailValidationSchema, firstnameValidationSchema, lastnameValidationSchema, phoneNumberValidationSchema, zipCodeValidationSchema } from '@/app/presentation/forms/validations/userValidationSchema';

@Component({
  imports: [FormTextInput, FormDateInput, FormCheckboxInput, FormRoot],
  selector: 'app-customer-edit-panel',
  styleUrl: './customer-edit-panel.scss',
  templateUrl: './customer-edit-panel.html',
})
export class CustomerEditPanel implements OnInit {

  constructor(public datepipe: DatePipe){}
  
  public ngOnInit(): void {
    this.customerModel.set({
      vatNumber: this.customer().vatNumber ?? '',
      firstName: this.customer().user.firstName,
      lastName: this.customer().user.lastName,
      email: this.customer().user.email ?? '',
      phoneNumber: this.customer().user.phoneNumber ?? '',
      address: this.customer().user.address ?? '',
      city: this.customer().user.city ?? '',
      zipCode: this.customer().user.zipCode ?? '',
      country: this.customer().user.country ?? '',
      birthDate: this.customer().user.birthDate,
      allowMarketing: this.customer().user.allowMarketing
    });
  }

  public customer = input.required<Customer>();
  public onUpdateCustomerEvent = output<{id: number, obj: CustomerUpdateDto}>();
  public onCancelUpdateCustomerEvent = output();
  
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
  protected customerModel = signal<UpdateCustomerFormData>({...this.INITIAL_MODEL});

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
        const customerUpdateDto: CustomerUpdateDto = {
          vatNumber: (model().value().vatNumber === "") ? null : model().value().vatNumber,
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
        this.onUpdateCustomerEvent.emit({id: this.customer().id, obj: customerUpdateDto});
        model().reset({...this.INITIAL_MODEL});
      }
    }
  });

  protected handleCancel(event: Event) {
    event.preventDefault();

    this.onCancelUpdateCustomerEvent.emit();
  }
}
