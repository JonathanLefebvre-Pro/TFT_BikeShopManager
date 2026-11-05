import { inject, Service } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { EmployeeReturnDto } from '@/app/appCore/dtos/employees/employeeReturnDto';
import { ILogUserRepository } from '@/app/appCore/abstraction/repositories/iloguser-repository';

@Service()
export class LogUserService implements ILogUserRepository {

    protected readonly baseUrl = 'https://localhost:7105';
    readonly http = inject(HttpClient);
    
    public getEmployee() : Observable<EmployeeReturnDto> {
        const url:string = `${this.baseUrl}/${"api/LogUser/GetCurrentAsync"}`;
        return this.http.get<EmployeeReturnDto>(url);
    }
}