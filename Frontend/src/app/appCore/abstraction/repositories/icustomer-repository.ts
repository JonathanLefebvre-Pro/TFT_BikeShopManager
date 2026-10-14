import { Customer } from "@/app/domain/customer";
import { AddCustomerDto } from "@/app/domain/dtos/addCustomerDto";
import { Observable } from "rxjs";

export interface ICustomerRepository {
    getCustomers() : Observable<Customer[]>;
    getCustomerById(id: number) : Observable<Customer>;
    addCustomer(customer: AddCustomerDto) : any;
}
