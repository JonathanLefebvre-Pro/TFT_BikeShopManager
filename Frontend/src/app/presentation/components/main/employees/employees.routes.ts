import { Routes } from "@angular/router";
import { EmployeesListPage } from "./pages/employees-list-page/employees-list-page";
import { EmployeesEditPage } from "./pages/employees-edit-page/employees-edit-page";
import { EmployeesAddPage } from "./pages/employees-add-page/employees-add-page";
import { EmployeeProfilePage } from "./pages/employee-profile-page/employee-profile-page";
import { RoleGuard } from "@/app/infrastructure/guards/RoleGuards";

export const routes: Routes = [
    {
        path: 'list',
        component: EmployeesListPage,
        canActivate: [RoleGuard],
        data: {roles : ['Admin', 'Manager', 'Seller']}
    },
    {
        path: 'edit/:id',
        component: EmployeesEditPage,
        canActivate: [RoleGuard],
        data: {roles : ['Admin', 'Manager']}
    },
    {
        path: 'add',
        component: EmployeesAddPage,
        canActivate: [RoleGuard],
        data: {roles : ['Admin']}
    },
    {
        path: 'profile',
        component: EmployeeProfilePage
    },
    {
        path: '',
        redirectTo: 'profile',
        pathMatch: 'full'
    }
];