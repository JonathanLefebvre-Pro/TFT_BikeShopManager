import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { EmployeeEditPanel } from '../employee-edit-panel/employee-edit-panel';
import { ActivatedRoute, Router } from '@angular/router';
import { EmployeeService } from '@/app/infrastructure/services/employee-service';
import { EmployeeDtoMapper } from '@/app/presentation/mappers/employees/employee-dto-mapper';
import { Employee } from '@/app/domain/employee';
import { EmployeeUpdateDto } from '@/app/appCore/dtos/employees/employeeUpdateDto';
import { IRoleRepository } from '@/app/appCore/abstraction/repositories/irole-repository';
import { RoleService } from '@/app/infrastructure/services/role-service';
import { Role } from '@/app/domain/role';
import { RoleDtoMapper } from '@/app/presentation/mappers/roles/role-dto-mapper';

@Component({
  imports: [EmployeeEditPanel],
  selector: 'app-employees-edit-page',
  styleUrl: './employees-edit-page.scss',
  templateUrl: './employees-edit-page.html',
})
export class EmployeesEditPage implements OnInit {
  
  private routeService = inject(ActivatedRoute);
  protected routerService = inject(Router);
  protected readonly employeeService : EmployeeService = inject(EmployeeService);
  protected readonly employeeDtoMapper : EmployeeDtoMapper = inject(EmployeeDtoMapper);
  protected readonly roleService : IRoleRepository = inject(RoleService);
  protected readonly roleDtoMapper : RoleDtoMapper = inject(RoleDtoMapper);

  protected readonly lastEditedEmployee = signal<Employee | null>(null);
  private readonly roles = signal<Role[]>([]);
  protected readonly roleOptions = computed(() => this.roles().map(x => x.name));

  ngOnInit(): void {

    this.roleService.getRoles().subscribe({
      next: (res) => {
        this.roles.set(this.roleDtoMapper.ToRoleEntities(res));
      },
      error: (error) => {
        console.error(error);
      }
    });

    this.routeService.params.subscribe((params) => {

      if(Number.isNaN(+params['id']))
        return;

      this.employeeService.getEmployeeById(+params['id']).subscribe({
        next: (res) => {
          this.lastEditedEmployee.set(this.employeeDtoMapper.ToEmployeeEntity(res));
        },
        error: (error) => {
          this.routerService.navigate(['..', 'edit/:id'], { relativeTo: this.routeService });
          console.error(error);
        }
      });
    });
  }

  protected handleUpdateEmployee(event : {id: number, obj: EmployeeUpdateDto}) {
    this.employeeService.updateEmployee(event.id, event.obj).subscribe({
        next: (res) => {
          this.exitEdit();
        },
        error: (error) => {
          console.error(error);
        }
    });
  }
  protected handleCancelUpdateEmployee() {
    this.exitEdit();
  }

  private exitEdit() {
    this.lastEditedEmployee.set(null);
    this.routerService.navigate(['../../list'], { relativeTo: this.routeService });
  }

  private isLoading(): boolean {
    return this.lastEditedEmployee() !== null;
  }
}
