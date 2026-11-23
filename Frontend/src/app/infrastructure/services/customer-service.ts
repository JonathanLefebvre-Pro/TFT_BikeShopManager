import { inject, Service } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ICustomerRepository } from '@/app/appCore/abstraction/repositories/icustomer-repository';
import { CustomerAddDto } from '@/app/appCore/dtos/customers/customerAddDto';
import { CustomerReturnDto } from '@/app/appCore/dtos/customers/customerReturnDto';
import { CustomerUpdateDto } from '@/app/appCore/dtos/customers/customerUpdateDto';
import { PageResponseDto } from '@/app/appCore/dtos/shared/pageResponseDto';

@Service()
export class CustomerService implements ICustomerRepository {
    
    // protected readonly baseUrl = 'https://localhost:7105';
    readonly http = inject(HttpClient);
    
    public getCustomers(pageNumber: number, pageSize: number, keywords: string | null) : Observable<PageResponseDto<CustomerReturnDto>> {
        const url:string = `/${"api/Customer/GetAsync"}?keywords=${keywords}&pageNumber=${pageNumber}&pageSize=${pageSize}`;
        return this.http.get<PageResponseDto<CustomerReturnDto>>(url);
    }

    public getCustomersCount(): Observable<number> {
        const url:string = `/${"api/Customer/GetCountAsync"}`;
        return this.http.get<number>(url);
    }
    
    public getCustomerById(id: number) : Observable<CustomerReturnDto> {
        const url:string = `/${"api/Customer/GetAsync"}/${id}`;
        return this.http.get<CustomerReturnDto>(url);
    }

    public addCustomer(customer: CustomerAddDto) : Observable<number> {
        const url:string = `/${"api/Customer/AddAsync"}`;
        return this.http.post<number>(url, customer);
    }

    public disableCustomer(id: number): Observable<HttpResponse<Object>> {
       const url:string = `/${"api/Customer/DisableAsync"}/${id}`;
       return this.http.patch(url, null, { observe: 'response' });
    }

    public enableCustomer(id: number): Observable<HttpResponse<Object>> {
       const url:string = `/${"api/Customer/EnableAsync"}/${id}`;
       return this.http.patch(url, null, { observe: 'response' });
    }

    public updateCustomer(id: number, customer: CustomerUpdateDto) : Observable<HttpResponse<Object>> {
        const url:string = `/${"api/Customer/UpdateAsync"}/${id}`;
        return this.http.put(url, customer, { observe: 'response' });
    }
}
