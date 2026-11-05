import { Component, computed, DestroyRef, inject, OnInit, signal } from '@angular/core';
import { EmployeeAddPanel } from '@presentation/components/main/employees/employee-add-panel/employee-add-panel';
import { Role } from '@/app/domain/role';
import { RoleDtoMapper } from '@/app/presentation/mappers/roles/role-dto-mapper';
import { RoleService } from '@/app/infrastructure/services/role-service';
import { IRoleRepository } from '@/app/appCore/abstraction/repositories/irole-repository';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

@Component({
  imports: [EmployeeAddPanel],
  selector: 'app-employees-add-page',
  styleUrl: './employees-add-page.scss',
  templateUrl: './employees-add-page.html',
})
export class EmployeesAddPage implements OnInit {

  protected readonly destroyRef = inject(DestroyRef);
  protected readonly roleService : IRoleRepository = inject(RoleService);
  protected readonly roleDtoMapper : RoleDtoMapper = inject(RoleDtoMapper);

  private readonly roles = signal<Role[]>([]);
  protected readonly roleOptions = computed(() => this.roles().map(x => x.name));

  ngOnInit(): void {
     this.roleService.getRoles().pipe(takeUntilDestroyed(this.destroyRef)).subscribe({
      next: (res) => {
        this.roles.set(this.roleDtoMapper.ToRoleEntities(res));
      },
      error: (error) => {
        console.error(error);
      }
    });
  }
}
