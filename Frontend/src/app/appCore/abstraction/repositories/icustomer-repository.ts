import { CustomerAddDto } from "@/app/appCore/dtos/customerAddDto";
import { CustomerReturnDto } from '@/app/appCore/dtos/customerReturnDto';
import { CustomerUpdateDto } from "@/app/appCore/dtos/customerUpdateDto";
import { HttpResponse } from "@angular/common/http";
import { Observable } from "rxjs";

export interface ICustomerRepository {
    getCustomers(offset: number, limit: number) : Observable<CustomerReturnDto[]>;
    getCustomersCount() : Observable<number>;
    getCustomerById(id: number) : Observable<CustomerReturnDto>;
    addCustomer(customer: CustomerAddDto) : Observable<number>;
    disableCustomer(id: number) : Observable<HttpResponse<Object>>;
    enableCustomer(id: number) : Observable<HttpResponse<Object>>;
    updateCustomer(id: number, customer: CustomerUpdateDto) : Observable<HttpResponse<Object>>;
    getCustomersByKeywords(keywords: string[]) : Observable<CustomerReturnDto[]>;
}
