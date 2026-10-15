import { Component, inject, OnInit, signal, model, output } from '@angular/core';
import { CustomerTableRow } from '../customer-table-row/customer-table-row';
import { CustomerService } from '../../../../infrastructure/services/customer-service';
import { Customer } from '../../../../domain/customer';
import { CustomerDtoMapper } from '@/app/presentation/mappers/customer-dto-mapper';

@Component({
  imports: [CustomerTableRow],
  selector: 'app-customers-table',
  styleUrl: './customers-table.scss',
  templateUrl: './customers-table.html',
})
export class CustomersTable implements OnInit {
  protected readonly customerService : CustomerService = inject(CustomerService);
  protected readonly customerDtoMapper : CustomerDtoMapper = inject(CustomerDtoMapper);
  readonly customers = signal<Customer[]>([]);
  readonly isLoaded = signal<boolean>(false);

  onEditCustomerEvent = output<Customer>();
  onSaveCustomerEvent = output();

  ngOnInit(): void {
    this.customerService.getCustomers().subscribe({
      next : (res) => {
        this.isLoaded.set(false);
        this.customers.set(this.customerDtoMapper.ToCustomerEntities(res));
        this.isLoaded.set(true);
      },
      error: (error) => {
        this.isLoaded.set(false);
        console.error(error);
      }
    })
  }

  handleCustomerEdit(customer: Customer) {
    //console.log("handle edit log from customers table : " + customer.id);
    this.onEditCustomerEvent.emit(customer);
  }

  handleCustomerSave() {
    //console.log("Handle save log from customers table");
    this.onSaveCustomerEvent.emit();
  }
}