import { Component, inject, OnInit, signal } from '@angular/core';
import { CustomerEditPanel } from '@presentation/components/main/customers/customer-edit-panel/customer-edit-panel';
import { Customer } from '@domain/customer';
import { CustomerService } from '@infrastructure/services/customer-service';
import { CustomerUpdateDto } from '@/app/appCore/dtos/customers/customerUpdateDto';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomerDtoMapper } from '@/app/presentation/mappers/customers/customer-dto-mapper';

@Component({
  imports: [CustomerEditPanel],
  selector: 'app-customers-page',
  styleUrl: './customers-edit-page.scss',
  templateUrl: './customers-edit-page.html',
})
export class CustomersEditPage implements OnInit {
  
  private routeService = inject(ActivatedRoute);
  protected routerService = inject(Router);
  protected readonly customerService : CustomerService = inject(CustomerService);
  protected readonly customerDtoMapper : CustomerDtoMapper = inject(CustomerDtoMapper);

  protected readonly lastEditedCustomer = signal<Customer | null>(null);

  ngOnInit(): void {
    this.routeService.params.subscribe((params) => {

      if(Number.isNaN(+params['id']))
        return;

      this.customerService.getCustomerById(+params['id']).subscribe({
        next: (res) => {
          this.lastEditedCustomer.set(this.customerDtoMapper.ToCustomerEntity(res))
        },
        error: (error) => {
          this.routerService.navigate(['..','edit/:id'], 
            { relativeTo: this.routeService });
          console.error(error);
        }
      });
    });
  }

  protected handleUpdateCustomer(event : {id: number, obj: CustomerUpdateDto}) {
    this.customerService.updateCustomer(event.id, event.obj).subscribe({
        next: (res) => {
          this.exitEdit();
        },
        error: (error) => {
          console.error(error);
        }
    });
  }
  protected handleCancelUpdateCustomer() {
    this.exitEdit();
  }

  private exitEdit() {
    this.lastEditedCustomer.set(null);
    this.routerService.navigate(['../../list'], 
      { relativeTo: this.routeService });
  }

  private isLoading(): boolean {
    return this.lastEditedCustomer() !== null;
  }
}
