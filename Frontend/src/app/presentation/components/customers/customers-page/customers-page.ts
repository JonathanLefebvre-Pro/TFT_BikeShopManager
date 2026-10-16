import { Component, ElementRef, inject, OnInit, signal, viewChild } from '@angular/core';
import { CustomersTable } from '../customers-table/customers-table';
import { AddCustomer } from '../add-customer/add-customer';
import { CustomerEditPanel } from '../customer-edit-panel/customer-edit-panel';
import { Customer } from '@/app/domain/customer';
import { CustomerService } from '@/app/infrastructure/services/customer-service';
import { CustomerDtoMapper } from '@/app/presentation/mappers/customer-dto-mapper';
import { CustomerUpdateDto } from '@/app/appCore/dtos/customerUpdateDto';

@Component({
  imports: [CustomersTable, AddCustomer, CustomerEditPanel],
  selector: 'app-customers-page',
  styleUrl: './customers-page.scss',
  templateUrl: './customers-page.html',
})
export class CustomersPage implements OnInit {
  protected readonly customerService : CustomerService = inject(CustomerService);
  protected readonly customerDtoMapper : CustomerDtoMapper = inject(CustomerDtoMapper);

  protected readonly customers = signal<Customer[]>([]);
  protected readonly lastEditedCustomer = signal<Customer | null>(null);

  protected readonly customersTableElement = viewChild(CustomersTable);
  protected readonly editPanelElement = viewChild(CustomerEditPanel);

  ngOnInit(): void {
    this.init();
  }

  init(){
    this.lastEditedCustomer.set(null);
    this.customerService.getCustomers().subscribe({
      next : (res) => {
        this.customers.set(this.customerDtoMapper.ToCustomerEntities(res));
      },
      error: (error) => {
        console.error(error);
      }
    })
  }

  handleEnableCustomer(customerId: number) {
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
  handleDisableCustomer(customerId: number) {
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
  handleEditCustomer(customerId: number) {
    this.lastEditedCustomer.set(null);

    if(this.customers() === null || this.customers().length <= 0)
      return;

    const customer: Customer | undefined = this.customers().find(c => c.id === customerId);
    if(customer !== null && customer !== undefined)
    {
      this.customersTableElement()?.notifyEditModeEnabled();
      this.lastEditedCustomer.set(customer);
    }
  }
  handleUpdateCustomer(event : {id: number, obj: CustomerUpdateDto}) {
    this.customerService.updateCustomer(event.id, event.obj).subscribe({
        next: (res) => {
          console.log("Result : " + JSON.stringify(res));
          this.init();
        },
        error: (error) => {
          console.log("Error : " + JSON.stringify(error));
          console.log(JSON.stringify(event.obj));
        }
    });
  }
  handleCancelUpdateCustomer() {
    this.init();
    //this.lastEditedCustomer.set(null);
  }
}
