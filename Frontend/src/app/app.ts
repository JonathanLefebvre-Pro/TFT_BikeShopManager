import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { DisplayCustomers } from './presentation/customers/display-customers/display-customers';

@Component({
  imports: [RouterOutlet, DisplayCustomers],
  selector: 'app-root',
  styleUrl: './app.scss',
  templateUrl: './app.html',
})
export class App {
  protected readonly title = signal('BikeShopManager');
}
