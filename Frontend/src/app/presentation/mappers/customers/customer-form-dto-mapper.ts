import { CustomerAddDto } from '@/app/appCore/dtos/customers/customerAddDto';
import { CustomerUpdateDto } from '@/app/appCore/dtos/customers/customerUpdateDto';
import { Service } from '@angular/core';
import { AddCustomerFormData } from '@presentation/forms/models/customers/addCustomerFormData';

@Service()
export class CustomerFormDtoMapper {

    public ToCustomerAddDto(formData: AddCustomerFormData) : CustomerAddDto {
        const customerAddDto: CustomerAddDto = {
            vatNumber: (formData.vatNumber === "") ? null : formData.vatNumber,
            firstName: formData.firstName,
            lastName: formData.lastName,
            email: (formData.email === "") ? null : formData.email,
            phoneNumber: (formData.phoneNumber === "") ? null : formData.phoneNumber,
            address: (formData.address === "") ? null : formData.address,
            city: (formData.city === "") ? null : formData.city,
            zipCode: (formData.zipCode === "") ? null : formData.zipCode,
            country: (formData.country === "") ? null : formData.country,
            birthDate: formData.birthDate?.toISOString().substring(0,10) ?? null,
            allowMarketing: formData.allowMarketing
        }
        return customerAddDto;
    }

    public ToCustomerUpdateDto(formData: AddCustomerFormData) : CustomerUpdateDto {
        const customerUpdateDto: CustomerUpdateDto = {
            vatNumber: (formData.vatNumber === "") ? null : formData.vatNumber,
            firstName: formData.firstName,
            lastName: formData.lastName,
            email: (formData.email === "") ? null : formData.email,
            phoneNumber: (formData.phoneNumber === "") ? null : formData.phoneNumber,
            address: (formData.address === "") ? null : formData.address,
            city: (formData.city === "") ? null : formData.city,
            zipCode: (formData.zipCode === "") ? null : formData.zipCode,
            country: (formData.country === "") ? null : formData.country,
            birthDate: formData.birthDate?.toISOString().substring(0,10) ?? null,
            allowMarketing: formData.allowMarketing
        }

        return customerUpdateDto;
    }
}
