import { Service } from '@angular/core';
import { AddEmployeeFormData } from '@presentation/forms/models/employees/addEmployeeFormData';
import { EmployeeAddDto } from '@/app/appCore/dtos/employees/employeeAddDto';
import { EmployeeUpdateDto } from '@/app/appCore/dtos/employees/employeeUpdateDto';
import { UpdateEmployeeFormData } from '../../forms/models/employees/updateEmployeeFormData';

@Service()
export class EmployeeFormDtoMapper {
    public ToEmployeeAddDto(formData: AddEmployeeFormData) : EmployeeAddDto {
        const employeeAddDto: EmployeeAddDto = {
            password: formData.password,
            hiredAt: formData.hiredAt.toISOString().substring(0, 10),
            contractEndAt: formData.contractEndAt?.toISOString().substring(0, 10) ?? null,
            firstName: formData.firstName,
            lastName: formData.lastName,
            email: (formData.email === "") ? null : formData.email,
            phoneNumber: (formData.phoneNumber === "") ? null : formData.phoneNumber,
            address: (formData.address === "") ? null : formData.address,
            city: (formData.city === "") ? null : formData.city,
            zipCode: (formData.zipCode === "") ? null : formData.zipCode,
            country: (formData.country === "") ? null : formData.country,
            birthDate: formData.birthDate?.toISOString().substring(0, 10) ?? null,
            // allowMarketing: formData.allowMarketing,
            roleId: formData.roleId
        }
        return employeeAddDto;
    }

    public ToEmployeeUpdateDto(formData: UpdateEmployeeFormData) : EmployeeUpdateDto {
        const employeeUpdateDto: EmployeeUpdateDto = {
            hiredAt: formData.hiredAt.toISOString().substring(0, 10),
            contractEndAt: formData.contractEndAt?.toISOString().substring(0, 10) ?? null,
            firstName: formData.firstName,
            lastName: formData.lastName,
            email: (formData.email === "") ? null : formData.email,
            phoneNumber: (formData.phoneNumber === "") ? null : formData.phoneNumber,
            address: (formData.address === "") ? null : formData.address,
            city: (formData.city === "") ? null : formData.city,
            zipCode: (formData.zipCode === "") ? null : formData.zipCode,
            country: (formData.country === "") ? null : formData.country,
            birthDate: formData.birthDate?.toISOString().substring(0,10) ?? null,
            // allowMarketing: formData.allowMarketing,
            roleId: formData.roleId
        }
        return employeeUpdateDto;
    }
}
