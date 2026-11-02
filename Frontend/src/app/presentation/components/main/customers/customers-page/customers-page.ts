import { Component } from '@angular/core';
import { RouterLink, RouterOutlet, RouterLinkActive } from '@angular/router';
import { TextTooltip } from '@/app/presentation/directives/text-tooltip';

@Component({
  imports: [RouterOutlet, RouterLink, RouterLinkActive, TextTooltip],
  selector: 'app-customers-page',
  styleUrl: './customers-page.scss',
  templateUrl: './customers-page.html',
})
export class CustomersPage {}
