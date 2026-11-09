import { Component, DestroyRef, inject, OnInit, signal, viewChild } from '@angular/core';
import { EmployeesTable } from '@presentation/components/main/employees/employees-table/employees-table';
import { EmployeeSearchInput } from '@presentation/components/main/employees/employee-search-input/employee-search-input';
import { EmployeeService } from '@/app/infrastructure/services/employee-service';
import { EmployeeDtoMapper } from '@/app/presentation/mappers/employees/employee-dto-mapper';
import { Employee } from '@/app/domain/employee';
import { ActivatedRoute, Router } from '@angular/router';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { AuthUserService } from '@/app/infrastructure/services/auth-user-service';

@Component({
  imports: [EmployeesTable, EmployeeSearchInput],
  selector: 'app-employees-list-page',
  styleUrl: './employees-list-page.scss',
  templateUrl: './employees-list-page.html',
})
export class EmployeesListPage implements OnInit {
  
  protected readonly routerService = inject(Router);
  protected readonly routeService = inject(ActivatedRoute);
  protected readonly destroyRef = inject(DestroyRef);
  protected readonly employeeService = inject(EmployeeService);
  protected readonly authUserService = inject(AuthUserService);
  protected readonly employeeDtoMapper = inject(EmployeeDtoMapper);

  protected readonly employees = signal<Employee[]>([]);
  protected readonly authUser = signal<Employee | null>(null);

  protected readonly keywords = signal<string>('');
  protected readonly totalPages = signal<number>(0);
  protected readonly pageNumber = signal<number>(0);
  protected readonly hasNextPage = signal<boolean>(false);
  protected readonly hasPreviousPage = signal<boolean>(false);
  protected readonly pageSize: number = 2;

  protected readonly employeesTableElement = viewChild(EmployeesTable);

  public ngOnInit(): void {
    this.init();
  }

  protected init(){
    this.pageNumber.set(1);
    this.keywords.set('');
    this.getEmployees();
  }

  protected handleEnableEmployee(employeeId: number) {
    this.employeeService.enableEmployee(employeeId)
    .pipe(takeUntilDestroyed(this.destroyRef))
    .subscribe({
      next: (res) => {
        console.log("Result : " + JSON.stringify(res.status));
        this.init();
      },
      error(err) {
        console.log("Error" + err);
      },
    });
  }
  protected handleDisableEmployee(employeeId: number) {
    this.employeeService.disableEmployee(employeeId)
    .pipe(takeUntilDestroyed(this.destroyRef))
    .subscribe({
      next: (res) => {
        console.log("Result : " + JSON.stringify(res.status));
        this.init();
      },
      error(error) {
        console.error("Error" + JSON.stringify(error));
      },
    });
  }

  protected handleEditEmployee(employeeId: number) {
    if(this.employees() === null || this.employees().length <= 0)
      return;

    const employee: Employee | undefined = this.employees().find(c => c.id === employeeId);
    if(employee !== null && employee !== undefined)
    {
      this.employeesTableElement()?.notifyEditModeEnabled();
      this.routerService.navigate(['..', 'edit', employee.id], { relativeTo: this.routeService });
    }
  }

  protected handleSearchEmployees(keywords : string) {
      this.keywords.set(keywords);
      this.pageNumber.set(1);
      this.getEmployees();
  }
  protected handleClearSearchEmployees() {
    this.init();
  }

  protected handleNextEmployeePage() {
    if(this.hasNextPage())
    {
      this.pageNumber.update((num) => num+1);
      this.getEmployees();
    }
  }
  protected handlePreviousEmployeePage() {
    if(this.hasPreviousPage())
    {
      this.pageNumber.update((num) => num-1);
      this.getEmployees();
    }
  }

  private getEmployees() {
    //Get auth employee data
    this.authUserService.getEmployee().subscribe({
      next: (res) => {
        this.authUser.set(this.employeeDtoMapper.ToEmployeeEntity(res));
      },
      error: (err) => {
        console.error(err);
      }
    });

    //Get employees data (with pagination and search)
    this.employeeService.getEmployees(this.pageNumber(),this.pageSize, this.keywords())
    .pipe(takeUntilDestroyed(this.destroyRef))
    .subscribe({
      next : (res) => {
        this.totalPages.set(res.totalPages);
        this.pageNumber.set(res.pageNumber);
        this.hasNextPage.set(res.hasNextPage);
        this.hasPreviousPage.set(res.hasPreviousPage);
        this.employees.set(this.employeeDtoMapper.ToEmployeeEntities(res.data));
    },
      error: (err) => {
        console.error(err);
        //If the search returned an error (there is no employees related to these keywords, display a empty list !)
        this.employees.set([]);
      }
    });
  }
}
