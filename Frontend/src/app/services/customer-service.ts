import { inject, Service } from '@angular/core';
import { Customer } from '../domain/customer';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Service()
export class CustomerService {
    protected readonly baseUrl = 'https://localhost:7105';
    readonly http = inject(HttpClient);

    public getCustomers() : Observable<Customer[]> {
        const url:string = `${this.baseUrl}/${"api/Customer/GetAsync"}`;
        return this.http.get<Customer[]>(url)
    }

    public getCustomersJson() {
        const url:string = `${this.baseUrl}/${"api/Customer/GetAsync"}`;
        this.http.get(url).subscribe((data) => {console.log(data)});
    }

    public getCustomerById(id: number) : Observable<Customer> {
        const url:string = `${this.baseUrl}/${"api/Customer/GetAsync"}/${id}`;
        return this.http.get<Customer>(url);
    }
}
