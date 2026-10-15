import { Customer } from '@/app/domain/customer';
import { User } from '@/app/domain/user';
import { CustomerReturnDto } from '../dtos/customerReturnDto';
import { Service } from '@angular/core';

@Service()
export class CustomerDtoMapper {

    public ToUserEntity(dto: CustomerReturnDto) : User {
        const user: User = {
            id: dto.userId,
            firstName: dto.firstName,
            lastName: dto.lastName,
            email: dto.email,
            phoneNumber: dto.phoneNumber,
            address: dto.address,
            city: dto.city,
            zipCode: dto.zipCode,
            country: dto.country,
            birthDate: dto.birthDate,
            allowMarketing: dto.allowMarketing
        };
        return user;
    }

    public ToCustomerEntity(dto: CustomerReturnDto) : Customer {
        const customer: Customer = {
            id: dto.customerId,
            userId: dto.userId,
            vatNumber: dto.vatNumber,
            isActive: dto.isActive,
            
            user: this.ToUserEntity(dto)
        };

        return customer;
    }

    public ToCustomerEntities(dtos: CustomerReturnDto[]) : Customer[] {
        
        let customerArray: Customer[] = [];
        for (let index = 0; index < dtos.length; index++) {
            const element = dtos[index];
            customerArray.push(this.ToCustomerEntity(element));
        }
        return customerArray;
    }
}