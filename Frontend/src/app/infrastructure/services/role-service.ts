import { IRoleRepository } from "@/app/appCore/abstraction/repositories/irole-repository";
import { RoleReturnDto } from "@/app/appCore/dtos/roles/roleReturnDto";
import { HttpClient } from "@angular/common/http";
import { inject, Service } from "@angular/core";
import { Observable } from "rxjs";

@Service()
export class RoleService implements IRoleRepository {

    // protected readonly baseUrl = 'https://localhost:7105';
    readonly http = inject(HttpClient);

    getRoles(): Observable<RoleReturnDto[]> {
        const url:string = `/${"api/Role/GetAsync"}`;
        return this.http.get<RoleReturnDto[]>(url);
    }
    getRoleById(id: number): Observable<RoleReturnDto> {
        const url:string = `/${"api/Role/GetAsync"}/${id}`;
        return this.http.get<RoleReturnDto>(url);
    }
}