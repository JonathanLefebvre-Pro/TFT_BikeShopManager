import { UserRoles } from '@/app/appCore/enum/role-enum';
import { AuthUserService } from '@/app/infrastructure/services/auth-user-service';
import { Component, DestroyRef, inject, OnInit, signal } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { RouterLink, RouterOutlet, RouterLinkActive } from '@angular/router';

@Component({
  imports: [RouterOutlet, RouterLink, RouterLinkActive],
  selector: 'app-employees-page',
  styleUrl: './employees-page.scss',
  templateUrl: './employees-page.html',
})
export class EmployeesPage implements OnInit {

  protected readonly authUserService = inject(AuthUserService);
  protected readonly destroyRef = inject(DestroyRef);
  protected readonly roleId = signal<number>(-1);

  protected UserRolesEnum = UserRoles;

  public ngOnInit(): void {
    this.authUserService.getRole()
    .pipe(takeUntilDestroyed(this.destroyRef))
    .subscribe({
      next: (res) => {
        //console.log('Role : ', res);
        this.roleId.set(res.id);
      },
      error: (err) => {
        console.error('Get AuthUser Role Error : ', err);
      }
    });
  }
}
