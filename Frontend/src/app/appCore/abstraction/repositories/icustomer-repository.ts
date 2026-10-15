import { CustomerAddDto } from "@/app/presentation/dtos/customerAddDto";
import { CustomerReturnDto } from '@/app/presentation/dtos/customerReturnDto';
import { Observable } from "rxjs";

export interface ICustomerRepository {
    getCustomers() : Observable<CustomerReturnDto[]>;
    getCustomerById(id: number) : Observable<CustomerReturnDto>;
    addCustomer(customer: CustomerAddDto) : Observable<number>;
    disableCustomer(customerId: number) : Observable<Object>;
    enableCustomer(customerId: number) : Observable<Object>;
}
