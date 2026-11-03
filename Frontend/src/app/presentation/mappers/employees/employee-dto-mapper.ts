import { EmployeeReturnDto } from '@/app/appCore/dtos/employees/employeeReturnDto';
import { Employee } from '@/app/domain/employee';
import { Role } from '@/app/domain/role';
import { User } from '@/app/domain/user';
import { Service } from '@angular/core';

@Service()
export class EmployeeDtoMapper {

    public ToUserEntity(dto: EmployeeReturnDto) : User {
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
            birthDate: (dto.birthDate) ? new Date(dto.birthDate) : null,
            allowMarketing: dto.allowMarketing
        };
        return user;
    }

    public ToRoleEntity(dto: EmployeeReturnDto) : Role {
        const role: Role = {
            id: dto.roleId,
            name: dto.roleName,
            normalizedName: dto.normalizedRoleName
        };
        return role;
    }
    
    public ToEmployeeEntity(dto: EmployeeReturnDto) : Employee {
        const employee: Employee = {
            id: dto.employeeId,
            userId: dto.userId,
            passwordHash: dto.passwordHash,
            securityStamp: dto.securityStamp,
            hiredAt: new Date(dto.hiredAt),
            contractEndAt: (dto.contractEndAt) ? new Date(dto.contractEndAt) : null, 
            isActive: dto.isActive,
            
            user: this.ToUserEntity(dto),

            role: this.ToRoleEntity(dto)
        };

        return employee;
    }
    
    public ToEmployeeEntities(dtos: EmployeeReturnDto[]) : Employee[] {
        
        let employeeArray: Employee[] = [];
        for (let index = 0; index < dtos.length; index++) {
            const element = dtos[index];
            employeeArray.push(this.ToEmployeeEntity(element));
        }
        return employeeArray;
    }
}
