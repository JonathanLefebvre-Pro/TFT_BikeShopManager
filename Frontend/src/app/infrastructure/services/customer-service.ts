import { inject, Service } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ICustomerRepository } from '@/app/appCore/abstraction/repositories/icustomer-repository';
import { CustomerAddDto } from '@/app/presentation/dtos/customerAddDto';
import { CustomerReturnDto } from '@/app/presentation/dtos/customerReturnDto';

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

    public addCustomer(customer: CustomerAddDto) : Observable<number> {
        const url:string = `${this.baseUrl}/${"api/Customer/AddAsync"}`;
        return this.http.post<number>(url, customer);
    }

    public disableCustomer(id: number): Observable<Object> {
       const url:string = `${this.baseUrl}/${"api/Customer/DisableAsync"}/${id}`;
       return this.http.patch(url, null);
    }

    public enableCustomer(id: number): Observable<Object> {
       const url:string = `${this.baseUrl}/${"api/Customer/EnableAsync"}/${id}`;
       return this.http.patch(url, null);
    }
}
