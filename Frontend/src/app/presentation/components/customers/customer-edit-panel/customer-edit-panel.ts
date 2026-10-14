import { Customer } from '@/app/domain/customer';
import { DatePipe } from '@angular/common';
import { Component, input } from '@angular/core';

@Component({
  imports: [DatePipe],
  selector: 'app-customer-edit-panel',
  styleUrl: './customer-edit-panel.scss',
  templateUrl: './customer-edit-panel.html',
})
export class CustomerEditPanel {
  //customer = input.required<Customer>();

  handleSave() {
    
  }
}
