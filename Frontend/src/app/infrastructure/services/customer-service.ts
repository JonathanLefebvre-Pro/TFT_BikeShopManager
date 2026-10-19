import { inject, Service } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ICustomerRepository } from '@/app/appCore/abstraction/repositories/icustomer-repository';
import { CustomerAddDto } from '@/app/appCore/dtos/customerAddDto';
import { CustomerReturnDto } from '@/app/appCore/dtos/customerReturnDto';
import { CustomerUpdateDto } from '@/app/appCore/dtos/customerUpdateDto';

@Service()
export class CustomerService implements ICustomerRepository {

    protected readonly baseUrl = 'https://localhost:7105';
    readonly http = inject(HttpClient);

    public getCustomers() : Observable<CustomerReturnDto[]> {
        const url:string = `${this.baseUrl}/${"api/Customer/GetAsync"}`;
        return this.http.get<CustomerReturnDto[]>(url)
    }

    public getCustomerById(id: number) : Observable<CustomerReturnDto> {
        const url:string = `${this.baseUrl}/${"api/Customer/GetAsync"}/${id}`;
        return this.http.get<CustomerReturnDto>(url);
    }

    public getCustomersByKeyword(keyword: string): Observable<CustomerReturnDto[]> {
        const url: string = `${this.baseUrl}/${"api/Customer/GetAsyncByKeyword"}`;
        return this.http.post<CustomerReturnDto[]>(url, { keyword: keyword });
    }

    public addCustomer(customer: CustomerAddDto) : Observable<number> {
        const url:string = `${this.baseUrl}/${"api/Customer/AddAsync"}`;
        return this.http.post<number>(url, customer);
    }

    public disableCustomer(id: number): Observable<HttpResponse<Object>> {
       const url:string = `${this.baseUrl}/${"api/Customer/DisableAsync"}/${id}`;
       return this.http.patch(url, null, { observe: 'response' });
    }

    public enableCustomer(id: number): Observable<HttpResponse<Object>> {
       const url:string = `${this.baseUrl}/${"api/Customer/EnableAsync"}/${id}`;
       return this.http.patch(url, null, { observe: 'response' });
    }
    public updateCustomer(id: number, customer: CustomerUpdateDto) : Observable<HttpResponse<Object>> {
        const url:string = `${this.baseUrl}/${"api/Customer/UpdateAsync"}/${id}`;
        return this.http.put(url, customer, { observe: 'response' });
    }
}
