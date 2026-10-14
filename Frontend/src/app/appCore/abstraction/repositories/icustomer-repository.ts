import { Customer } from "@/app/domain/customer";
import { AddCustomerDto } from "@/app/presentation/dtos/addCustomerDto";
import { Observable } from "rxjs";

export interface ICustomerRepository {
    getCustomers() : Observable<Customer[]>;
    getCustomerById(id: number) : Observable<Customer>;
    addCustomer(customer: AddCustomerDto) : Observable<number>;
    disableCustomer(customerId: number) : Observable<Object>;
    enableCustomer(customerId: number) : Observable<Object>;
}
