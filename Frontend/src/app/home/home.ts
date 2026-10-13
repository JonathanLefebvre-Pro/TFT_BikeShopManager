import { Component, inject, OnInit, signal } from '@angular/core';
import { CustomerFormRow } from '../forms/customer-form-row/customer-form-row';
import { UserFormRow } from '../forms/user-form-row/user-form-row';
import { CustomerService } from '../services/customer-service';
import { Customer } from '../domain/customer';

@Component({
  imports: [CustomerFormRow, UserFormRow],
  selector: 'app-home',
  styleUrl: './home.scss',
  templateUrl: './home.html',
})
export class Home implements OnInit {
  protected readonly customerService : CustomerService = inject(CustomerService);
  readonly customers = signal<Customer[]>([])

  ngOnInit(): void {
    this.customerService.getCustomers().subscribe({
      next : (res) => {
        this.customers.set(res);
      },
      error: (error) => {
        console.log(error);
      }
    })
  }
}