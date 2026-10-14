import { Customer } from "@/app/domain/customer";
import { Observable } from "rxjs";

export interface ICustomerRepository {
    getCustomers() : Observable<Customer[]>;
    getCustomerById(id: number) : Observable<Customer>
}
