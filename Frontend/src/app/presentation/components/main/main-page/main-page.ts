import { AuthService } from '@/app/infrastructure/services/auth-service';
import { Component, inject, signal } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';

@Component({
  imports: [RouterOutlet, RouterLink, RouterLinkActive],
  selector: 'app-main-page',
  styleUrl: './main-page.scss',
  templateUrl: './main-page.html',
})
export class MainPage {
  protected readonly title = signal('BikeShopManager');
  protected readonly authService = inject(AuthService);

  protected onLogout(event: Event) {
    event.preventDefault();
    this.authService.logout().subscribe();
  }
}
