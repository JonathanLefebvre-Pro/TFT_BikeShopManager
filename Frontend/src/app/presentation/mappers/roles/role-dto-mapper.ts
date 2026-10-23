import { RoleReturnDto } from '@/app/appCore/dtos/roles/roleReturnDto';
import { Role } from '@/app/domain/role';
import { Service } from '@angular/core';

@Service()
export class RoleDtoMapper {

    public ToRoleEntity(dto: RoleReturnDto) : Role {
        const role: Role = {
            id: dto.id,
            name: dto.name,
            normalizedName: dto.normalizedName
        }
        return role;
    }

    public ToRoleEntities(dtos: RoleReturnDto[]) : Role[] {
        let roleArray: Role[] = [];
        for (let index = 0; index < dtos.length; index++) {
            const element = dtos[index];
            roleArray.push(this.ToRoleEntity(element));
        }
        return roleArray;
    }
}
