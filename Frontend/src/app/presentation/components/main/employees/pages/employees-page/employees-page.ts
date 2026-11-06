import { Component } from '@angular/core';
import { RouterLink, RouterOutlet, RouterLinkActive } from '@angular/router';

@Component({
  imports: [RouterOutlet, RouterLink, RouterLinkActive],
  selector: 'app-employees-page',
  styleUrl: './employees-page.scss',
  templateUrl: './employees-page.html',
})
export class EmployeesPage {
}
