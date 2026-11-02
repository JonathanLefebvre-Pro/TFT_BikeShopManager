import { Observable } from "rxjs";
import { RoleReturnDto } from "../../dtos/roles/roleReturnDto";

export interface IRoleRepository {
    getRoles() : Observable<RoleReturnDto[]>;
    getRoleById(id: number) : Observable<RoleReturnDto>;
}