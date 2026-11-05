import { UpdatePasswordDto } from '@/app/appCore/dtos/auth-user/updatePasswordDto';
import { Employee } from '@/app/domain/employee';
import { AuthUserService } from '@/app/infrastructure/services/auth-user-service';
import { EmployeeDtoMapper } from '@/app/presentation/mappers/employees/employee-dto-mapper';
import { Component, DestroyRef, inject, OnInit, signal, viewChild } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { EmployeeProfileEditPanel } from '@presentation/components/main/employees/employee-profile-edit-panel/employee-profile-edit-panel';

@Component({
  imports: [EmployeeProfileEditPanel],
  selector: 'app-employee-profile-page',
  styleUrl: './employee-profile-page.scss',
  templateUrl: './employee-profile-page.html',
})
export class EmployeeProfilePage implements OnInit {
  
  protected readonly authUserService = inject(AuthUserService);
  protected readonly employeeDtoMapper = inject(EmployeeDtoMapper);
  
  protected readonly destroyRef = inject(DestroyRef);
  protected readonly employee = signal<Employee | null>(null);

  protected readonly employeeProfileEditPanelChild = viewChild(EmployeeProfileEditPanel);

  public ngOnInit(): void {
    //Recover current authenticated employee
    this.authUserService.getEmployee()
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

  public handleUpdateProfilePassword(obj: {currentPassword: string, newPassword: string}) {
    const dto: UpdatePasswordDto = {
      OldPassword: obj.currentPassword,
      NewPassword: obj.newPassword
    }

    this.authUserService.updateEmployeePassword(dto)
    .pipe(takeUntilDestroyed(this.destroyRef))
    .subscribe({
      next: (res) => {
        console.log(res);
        this.employeeProfileEditPanelChild()?.ResetForm();
      },
      error: (err) => {
        console.error(err);
        this.employeeProfileEditPanelChild()?.DisplayErrorMessage();
      }
    })
  }
}
