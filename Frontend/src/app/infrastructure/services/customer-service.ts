import { inject, Service } from '@angular/core';
import { Customer } from '../../domain/customer';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ICustomerRepository } from '@/app/appCore/abstraction/repositories/icustomer-repository';
import { AddCustomerDto } from '@/app/domain/dtos/addCustomerDto';

@Service()
export class CustomerService implements ICustomerRepository {
 
    protected readonly baseUrl = 'https://localhost:7105';
    readonly http = inject(HttpClient);

    public getCustomers() : Observable<Customer[]> {
        const url:string = `${this.baseUrl}/${"api/Customer/GetAsync"}`;
        return this.http.get<Customer[]>(url)
    }

    public getCustomerById(id: number) : Observable<Customer> {
        const url:string = `${this.baseUrl}/${"api/Customer/GetAsync"}/${id}`;
        return this.http.get<Customer>(url);
    }

    public addCustomer(customer: AddCustomerDto) : Observable<number> {
        const url:string = `${this.baseUrl}/${"api/Customer/AddAsync"}`;
        return this.http.post<number>(url, customer);
    }
}
