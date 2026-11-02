import { Component, model, output } from '@angular/core';

@Component({
  imports: [],
  selector: 'app-employee-search-input',
  styleUrl: './employee-search-input.scss',
  templateUrl: './employee-search-input.html',
})
export class EmployeeSearchInput {

  public onSearchEmployees = output<string>();
  public onClearSearchEmployees = output();
  
  protected inputValue = model<string>('');

  protected handleSubmit() {
    this.onSearchEmployees.emit(this.inputValue());
  }
  protected handleClear() {
    if(this.inputValue() === '')
      return;
    
    this.inputValue.set('');
    this.onClearSearchEmployees.emit();
  }
}
