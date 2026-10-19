import { Component, model, output } from '@angular/core';

@Component({
  imports: [],
  selector: 'app-customer-search-input',
  styleUrl: './customer-search-input.scss',
  templateUrl: './customer-search-input.html',
})
export class CustomerSearchInput {

  public onSearchCustomer = output<string>();
  
  protected inputValue = model<string>('');

  protected handleSubmit() {
    this.onSearchCustomer.emit(this.inputValue());
    console.log("Search input value : " + this.inputValue());
  }
}
