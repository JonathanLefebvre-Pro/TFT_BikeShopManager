import { Component, inject, OnInit, signal } from '@angular/core';
import { CustomerFormRow } from '../forms/customer-form-row/customer-form-row';
import { UserFormRow } from '../forms/user-form-row/user-form-row';
import { CustomerService } from '../services/customer-service';
import { AsyncPipe, JsonPipe } from '@angular/common';
import { Customer } from '../domain/customer';

@Component({
  imports: [CustomerFormRow, UserFormRow, AsyncPipe, JsonPipe],
  selector: 'app-home',
  styleUrl: './home.scss',
  templateUrl: './home.html',
})
export class Home implements OnInit {
  protected readonly customerService : CustomerService = inject(CustomerService);

  //readonly customers$ = this.customerService.getCustomers()
  readonly customers$2 = signal<Customer[]>([])

  // constructor() {
  //   this.customerService.getCustomersJson();
  // }

  ngOnInit(): void {
    this.customerService.getCustomers().subscribe({
      next : (res) => {
        this.customers$2.set(res);
        //console.log(res);
        //console.log(this.customers$2());
      },
      error: (error) => {
        console.log(error);
      }
    })

    //console.log(this);
  }
}