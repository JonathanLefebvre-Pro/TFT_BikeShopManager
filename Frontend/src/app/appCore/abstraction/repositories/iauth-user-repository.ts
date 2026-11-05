import { Observable } from "rxjs";
import { EmployeeReturnDto } from "@appCore/dtos/employees/employeeReturnDto";
import { HttpResponse } from "@angular/common/http";
import { UpdatePasswordDto } from "@appCore/dtos/auth-user/updatePasswordDto";

export interface IAuthUserRepository {
    getEmployee() : Observable<EmployeeReturnDto>;
    updateEmployeePassword(dto: UpdatePasswordDto): Observable<HttpResponse<Object>>;
}