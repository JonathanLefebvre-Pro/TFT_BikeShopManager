import { AuthService } from '@/app/infrastructure/services/auth-service';
import { Component, DestroyRef, inject, signal } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
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
  protected readonly destroyRef = inject(DestroyRef);

  protected onLogout(event: Event) {
    event.preventDefault();
    this.authService.logout().pipe(takeUntilDestroyed(this.destroyRef)).subscribe();
  }
}
