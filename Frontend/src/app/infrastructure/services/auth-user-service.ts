import { inject, Service } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { EmployeeReturnDto } from '@/app/appCore/dtos/employees/employeeReturnDto';
import { IAuthUserRepository as IAuthUserRepository } from '@/app/appCore/abstraction/repositories/iauth-user-repository';
import { UpdatePasswordDto } from '@/app/appCore/dtos/auth-user/updatePasswordDto';
import { RoleReturnDto } from '@/app/appCore/dtos/roles/roleReturnDto';

@Service()
export class AuthUserService implements IAuthUserRepository {
    
    protected readonly baseUrl = 'https://localhost:7105';
    readonly http = inject(HttpClient);
    
    public getEmployee() : Observable<EmployeeReturnDto> {
        const url:string = `${this.baseUrl}/${"api/AuthUser/GetUserAsync"}`;
        return this.http.get<EmployeeReturnDto>(url);
    }

    public updateEmployeePassword(dto: UpdatePasswordDto): Observable<HttpResponse<Object>> {
        const url:string = `${this.baseUrl}/${"api/AuthUser/UpdatePasswordAsync"}`;
        return this.http.patch(url, dto, { observe: 'response' });
    }

    public getRole(): Observable<RoleReturnDto> {
        const url:string = `${this.baseUrl}/${"api/AuthUser/GetRoleAsync"}`;
        return this.http.get<RoleReturnDto>(url);
    }
}