import { Component, inject, OnInit, signal, viewChild } from '@angular/core';
import { CustomersTable } from '@presentation/components/customers/customers-table/customers-table';
import { Customer } from '@domain/customer';
import { CustomerService } from '@infrastructure/services/customer-service';
import { CustomerDtoMapper } from '@presentation/mappers/customers/customer-dto-mapper';
import { CustomerSearchInput } from '../customer-search-input/customer-search-input';
import { Router } from '@angular/router';

@Component({
  imports: [CustomersTable, CustomerSearchInput],
  selector: 'app-customers-list-page',
  styleUrl: './customers-list-page.scss',
  templateUrl: './customers-list-page.html',
})
export class CustomersListPage implements OnInit {
  
  protected routerService = inject(Router);
  protected readonly customerService : CustomerService = inject(CustomerService);
  protected readonly customerDtoMapper : CustomerDtoMapper = inject(CustomerDtoMapper);

  protected readonly customers = signal<Customer[]>([]);

  protected readonly keywords = signal<string>('');
  protected readonly totalPages = signal<number>(0);
  protected readonly pageNumber = signal<number>(0);
  protected readonly hasNextPage = signal<boolean>(false);
  protected readonly hasPreviousPage = signal<boolean>(false);
  protected readonly pageSize: number = 2;

  protected readonly customersTableElement = viewChild(CustomersTable);

  public ngOnInit(): void {
    this.init();
  }

  protected init(){
    this.pageNumber.set(1);
    this.keywords.set('');
    this.getCustomers();
  }

  protected handleEnableCustomer(customerId: number) {
    this.customerService.enableCustomer(customerId).subscribe({
      next: (res) => {
        console.log("Result : " + JSON.stringify(res.status));
        this.init();
      },
      error(err) {
        console.log("Error" + err);
      },
    });
  }
  protected handleDisableCustomer(customerId: number) {
    this.customerService.disableCustomer(customerId).subscribe({
      next: (res) => {
        console.log("Result : " + JSON.stringify(res.status));
        this.init();
      },
      error(error) {
        console.error("Error" + error);
      },
    });
  }

  protected handleEditCustomer(customerId: number) {
    if(this.customers() === null || this.customers().length <= 0)
      return;

    const customer: Customer | undefined = this.customers().find(c => c.id === customerId);
    if(customer !== null && customer !== undefined)
    {
      this.customersTableElement()?.notifyEditModeEnabled();
      this.routerService.navigate(['/customers/edit', customer.id]);
    }
  }

  protected handleSearchCustomers(keywords : string) {

      this.keywords.set(keywords);
      this.pageNumber.set(1);
      this.getCustomers();
  }
  protected handleClearSearchCustomers() {
    this.init();
  }

  protected handleNextCustomerPage() {
    if(this.hasNextPage())
    {
      this.pageNumber.update((num) => num+1);
      this.getCustomers();
    }
  }
  protected handlePreviousCustomerPage() {
    if(this.hasPreviousPage())
    {
      this.pageNumber.update((num) => num-1);
      this.getCustomers();
    }
  }

  private getCustomers() {
    this.customerService.getCustomers(this.pageNumber(),this.pageSize, this.keywords()).subscribe({
      next : (res) => {
        this.totalPages.set(res.totalPages);
        this.pageNumber.set(res.pageNumber);
        this.hasNextPage.set(res.hasNextPage);
        this.hasPreviousPage.set(res.hasPreviousPage);
        this.customers.set(this.customerDtoMapper.ToCustomerEntities(res.data));
    },
      error: (error) => {
        console.error(error);
        //If the search returned an error (there is no customers related to these keywords, display a empty list !)
        this.customers.set([]);
      }
    });
  }
}
