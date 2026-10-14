import { Component, inject, OnInit, signal } from '@angular/core';
import { CustomerTableRow } from '../customer-table-row/customer-table-row';
import { UserTableRow } from '../user-table-row/user-table-row';
import { CustomerService } from '../../../infrastructure/services/customer-service';
import { Customer } from '../../../domain/customer';

@Component({
  imports: [CustomerTableRow, UserTableRow],
  selector: 'app-display-customers',
  styleUrl: './display-customers.scss',
  templateUrl: './display-customers.html',
})
export class DisplayCustomers implements OnInit {
  protected readonly customerService : CustomerService = inject(CustomerService);
  readonly customers = signal<Customer[]>([]);
  readonly isLoaded = signal<boolean>(false);

  ngOnInit(): void {
    this.customerService.getCustomers().subscribe({
      next : (res) => {
        this.isLoaded.set(false);
        this.customers.set(res);
        this.isLoaded.set(true);
      },
      error: (error) => {
        this.isLoaded.set(false);
      }
    })
  }
}