import { Observable } from "rxjs";
import { EmployeeReturnDto } from "../../dtos/employees/employeeReturnDto";

export interface ILogUserRepository {
    getEmployee() : Observable<EmployeeReturnDto>;
}