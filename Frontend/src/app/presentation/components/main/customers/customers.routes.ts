import { Routes } from "@angular/router";
import { CustomersListPage } from "./customers-list-page/customers-list-page";
import { CustomersEditPage } from "./customers-edit-page/customers-edit-page";
import { CustomersAddPage } from "./customers-add-page/customers-add-page";

export const routes: Routes = [
    {
        path: 'list',
        component: CustomersListPage,
    },
    {
        path: 'edit/:id',
        component: CustomersEditPage,
    },
    {
        path: 'add',
        component: CustomersAddPage,
    },
    {
        path: '',
        redirectTo: 'list',
        pathMatch: 'full'
    }
];