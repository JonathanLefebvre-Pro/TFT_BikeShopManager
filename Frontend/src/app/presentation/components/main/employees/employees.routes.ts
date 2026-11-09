import { Routes } from "@angular/router";
import { EmployeesListPage } from "./pages/employees-list-page/employees-list-page";
import { EmployeesEditPage } from "./pages/employees-edit-page/employees-edit-page";
import { EmployeesAddPage } from "./pages/employees-add-page/employees-add-page";
import { EmployeeProfilePage } from "./pages/employee-profile-page/employee-profile-page";

export const routes: Routes = [
    {
        path: 'list',
        component: EmployeesListPage,
        //TODO : Add a guard that check if the user has the right to display the list
    },
    {
        path: 'edit/:id',
        component: EmployeesEditPage,
        //TODO : Add a guard that check if the user has the right to edit
    },
    {
        path: 'add',
        component: EmployeesAddPage,
         //TODO : Add a guard that check if the user has the right to add
    },
    {
        path: 'profile',
        component: EmployeeProfilePage,
    },
    {
        path: '',
        redirectTo: 'profile',
        pathMatch: 'full'
    }
];