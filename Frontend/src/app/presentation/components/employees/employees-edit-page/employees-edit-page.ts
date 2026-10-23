import { Component, inject, OnInit, signal } from '@angular/core';
import { EmployeeEditPanel } from '../employee-edit-panel/employee-edit-panel';
import { ActivatedRoute, Router } from '@angular/router';
import { EmployeeService } from '@/app/infrastructure/services/employee-service';
import { EmployeeDtoMapper } from '@/app/presentation/mappers/employees/employee-dto-mapper';
import { Employee } from '@/app/domain/employee';
import { EmployeeUpdateDto } from '@/app/appCore/dtos/employees/employeeUpdateDto';

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

  protected readonly lastEditedEmployee = signal<Employee | null>(null);

  ngOnInit(): void {
    this.routeService.params.subscribe((params) => {

      if(Number.isNaN(+params['id']))
        return;

      this.employeeService.getEmployeeById(+params['id']).subscribe({
        next: (res) => {
          this.lastEditedEmployee.set(this.employeeDtoMapper.ToEmployeeEntity(res))
        },
        error: (error) => {
          this.routerService.navigate(['/employees/edit/:id']);
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
    this.routerService.navigate(['/employees/list']);
  }

  private isLoading(): boolean {
    return this.lastEditedEmployee() !== null;
  }
}
