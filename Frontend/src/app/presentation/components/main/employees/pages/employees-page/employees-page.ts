import { UserRoles } from '@/app/appCore/enum/role-enum';
import { AuthService } from '@/app/infrastructure/services/auth-service';
import { Component, inject } from '@angular/core';
import { RouterLink, RouterOutlet, RouterLinkActive } from '@angular/router';

@Component({
  imports: [RouterOutlet, RouterLink, RouterLinkActive],
  selector: 'app-employees-page',
  styleUrl: './employees-page.scss',
  templateUrl: './employees-page.html',
})
export class EmployeesPage {

  protected readonly authService = inject(AuthService);
  protected UserRolesEnum = UserRoles;

  protected hasAnyRoles(roles: UserRoles[]) {
    return this.authService.hasAnyRoles<UserRoles>(roles);
  }
}
