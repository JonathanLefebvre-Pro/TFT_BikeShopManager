import { Observable } from "rxjs";
import { PageResponseDto } from "../../dtos/shared/pageResponseDto";
import { EmployeeReturnDto } from "../../dtos/employees/employeeReturnDto";
import { EmployeeAddDto } from "../../dtos/employees/employeeAddDto";
import { HttpResponse } from "@angular/common/http";
import { EmployeeUpdateDto } from "../../dtos/employees/employeeUpdateDto";

export interface IEmployeeRepository {
    getEmployees(pageNumber: number, pageSize: number, keywords: string | null) : Observable<PageResponseDto<EmployeeReturnDto>>;
    getEmployeesCount() : Observable<number>;
    getEmployeesByRoleId(id: number) : Observable<EmployeeReturnDto[]>;
    getEmployeeById(id: number) : Observable<EmployeeReturnDto>;
    addEmployee(employee: EmployeeAddDto) : Observable<number>;
    disableEmployee(id: number) : Observable<HttpResponse<Object>>;
    enableEmployee(id: number) : Observable<HttpResponse<Object>>;
    updateEmployee(id: number, employee: EmployeeUpdateDto) : Observable<HttpResponse<Object>>;
}
