import { Component, inject, signal } from '@angular/core';
import { CustomerEditPanel } from '@presentation/components/customers/customer-edit-panel/customer-edit-panel';
import { Customer } from '@domain/customer';
import { CustomerService } from '@infrastructure/services/customer-service';
import { CustomerUpdateDto } from '@appCore/dtos/customerUpdateDto';
import { Router } from '@angular/router';

@Component({
  imports: [CustomerEditPanel],
  selector: 'app-customers-page',
  styleUrl: './customers-edit-page.scss',
  templateUrl: './customers-edit-page.html',
})
export class CustomersEditPage {

  protected routerService = inject(Router);
  protected readonly customerService : CustomerService = inject(CustomerService);

  protected readonly lastEditedCustomer = signal<Customer | null>(null);

  constructor() {
    const nav = this.routerService.currentNavigation();
    const state = nav?.extras?.state as Customer;
    console.log(state);

    if(state !== null)
      this.lastEditedCustomer.set(state);
  }

  protected handleUpdateCustomer(event : {id: number, obj: CustomerUpdateDto}) {
    this.customerService.updateCustomer(event.id, event.obj).subscribe({
        next: (res) => {
          console.log("Result : " + JSON.stringify(res));
          this.exitEdit();
        },
        error: (error) => {
          console.log("Error : " + JSON.stringify(error));
          console.log(JSON.stringify(event.obj));
        }
    });
  }
  protected handleCancelUpdateCustomer() {
    this.exitEdit();
  }

  private exitEdit() {
    this.lastEditedCustomer.set(null);
    this.routerService.navigate(['/customers/list']);
  }
}
