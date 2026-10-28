import { Routes } from "@angular/router";
import { EmployeesListPage } from "./employees-list-page/employees-list-page";
import { EmployeesEditPage } from "./employees-edit-page/employees-edit-page";
import { EmployeesAddPage } from "./employees-add-page/employees-add-page";

export const routes: Routes = [
    {
        path: 'list',
        component: EmployeesListPage,
    },
    {
        path: 'edit/:id',
        component: EmployeesEditPage,
    },
    {
        path: 'add',
        component: EmployeesAddPage,
    },
    {
        path: '',
        redirectTo: 'list',
        pathMatch: 'full'
    }
];