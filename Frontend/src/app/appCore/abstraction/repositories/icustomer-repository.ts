import { CustomerAddDto } from "@/app/appCore/dtos/customers/customerAddDto";
import { CustomerReturnDto } from '@/app/appCore/dtos/customers/customerReturnDto';
import { CustomerUpdateDto } from "@/app/appCore/dtos/customers/customerUpdateDto";
import { HttpResponse } from "@angular/common/http";
import { Observable } from "rxjs";
import { PageResponseDto } from "../../dtos/shared/pageResponseDto";

export interface ICustomerRepository {
    getCustomers(pageNumber: number, pageSize: number, keywords: string | null) : Observable<PageResponseDto<CustomerReturnDto>>;
    getCustomersCount() : Observable<number>;
    getCustomerById(id: number) : Observable<CustomerReturnDto>;
    addCustomer(customer: CustomerAddDto) : Observable<number>;
    disableCustomer(id: number) : Observable<HttpResponse<Object>>;
    enableCustomer(id: number) : Observable<HttpResponse<Object>>;
    updateCustomer(id: number, customer: CustomerUpdateDto) : Observable<HttpResponse<Object>>;
}
