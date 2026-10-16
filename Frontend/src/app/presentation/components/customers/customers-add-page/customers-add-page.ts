import { Component } from '@angular/core';
import { CustomerAddPanel } from '../customer-add-panel/customer-add-panel';

@Component({
  imports: [CustomerAddPanel],
  selector: 'app-customers-add-page',
  styleUrl: './customers-add-page.scss',
  templateUrl: './customers-add-page.html',
})
export class CustomersAddPage {}
