import { Component, model, output } from '@angular/core';

@Component({
  imports: [],
  selector: 'app-customer-search-input',
  styleUrl: './customer-search-input.scss',
  templateUrl: './customer-search-input.html',
})
export class CustomerSearchInput {

  public onSearchCustomers = output<string>();
  public onClearSearchCustomers = output();
  
  protected inputValue = model<string>('');

  protected handleSubmit() {
    this.onSearchCustomers.emit(this.inputValue());
  }
  protected handleClear() {
    this.inputValue.set('');
    this.onClearSearchCustomers.emit();
  }
}
