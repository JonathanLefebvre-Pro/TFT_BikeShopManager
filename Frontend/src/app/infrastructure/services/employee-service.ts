import { inject, Service } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PageResponseDto } from '@/app/appCore/dtos/shared/pageResponseDto';
import { IEmployeeRepository } from '@/app/appCore/abstraction/repositories/iemployee-repository';
import { EmployeeReturnDto } from '@/app/appCore/dtos/employees/employeeReturnDto';
import { EmployeeAddDto } from '@/app/appCore/dtos/employees/employeeAddDto';
import { EmployeeUpdateDto } from '@/app/appCore/dtos/employees/employeeUpdateDto';

@Service()
export class EmployeeService implements IEmployeeRepository {

    
    protected readonly baseUrl = 'https://localhost:7105';
    readonly http = inject(HttpClient);
    
    public getEmployees(pageNumber: number, pageSize: number, keywords: string | null) : Observable<PageResponseDto<EmployeeReturnDto>> {
        const url:string = `${this.baseUrl}/${"api/Employee/GetAsync"}?keywords=${keywords}&pageNumber=${pageNumber}&pageSize=${pageSize}`;
        return this.http.get<PageResponseDto<EmployeeReturnDto>>(url);
    }

    public getEmployeesCount(): Observable<number> {
        const url:string = `${this.baseUrl}/${"api/Employee/GetCountAsync"}`;
        return this.http.get<number>(url);
    }

    public getEmployeesByRoleId(id: number): Observable<EmployeeReturnDto[]> {
        const url:string = `${this.baseUrl}/${"api/Employee/GetByRoleIdAsync"}/${id}`;
        return this.http.get<EmployeeReturnDto[]>(url);
    }

    public getEmployeeById(id: number) : Observable<EmployeeReturnDto> {
        const url:string = `${this.baseUrl}/${"api/Employee/GetAsync"}/${id}`;
        return this.http.get<EmployeeReturnDto>(url);
    }

    public addEmployee(Employee: EmployeeAddDto) : Observable<number> {
        const url:string = `${this.baseUrl}/${"api/Employee/AddAsync"}`;
        return this.http.post<number>(url, Employee);
    }

    public disableEmployee(id: number): Observable<HttpResponse<Object>> {
       const url:string = `${this.baseUrl}/${"api/Employee/DisableAsync"}/${id}`;
       return this.http.patch(url, null, { observe: 'response' });
    }

    public enableEmployee(id: number): Observable<HttpResponse<Object>> {
       const url:string = `${this.baseUrl}/${"api/Employee/EnableAsync"}/${id}`;
       return this.http.patch(url, null, { observe: 'response' });
    }

    public updateEmployee(id: number, Employee: EmployeeUpdateDto) : Observable<HttpResponse<Object>> {
        const url:string = `${this.baseUrl}/${"api/Employee/UpdateAsync"}/${id}`;
        return this.http.put(url, Employee, { observe: 'response' });
    }
}
