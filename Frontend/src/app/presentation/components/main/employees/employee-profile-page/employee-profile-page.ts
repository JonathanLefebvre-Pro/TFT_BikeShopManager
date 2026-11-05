import { Employee } from '@/app/domain/employee';
import { LogUserService } from '@/app/infrastructure/services/loguser-service';
import { EmployeeDtoMapper } from '@/app/presentation/mappers/employees/employee-dto-mapper';
import { Component, DestroyRef, inject, OnInit, signal } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

@Component({
  imports: [],
  selector: 'app-employee-profile-page',
  styleUrl: './employee-profile-page.scss',
  templateUrl: './employee-profile-page.html',
})
export class EmployeeProfilePage implements OnInit {
  
  protected readonly logUserService = inject(LogUserService);
  protected readonly employeeDtoMapper = inject(EmployeeDtoMapper);
  
  protected readonly destroyRef = inject(DestroyRef);
  protected readonly employee = signal<Employee | null>(null);

  public ngOnInit(): void {
    //Recover current authenticated employee
    this.logUserService.getEmployee()
    .pipe(takeUntilDestroyed(this.destroyRef))
    .subscribe({
      next: (res) => {
        this.employee.set(this.employeeDtoMapper.ToEmployeeEntity(res));
      },
      error: (err) => {
        console.error(err);
      }
    })
  }
}
