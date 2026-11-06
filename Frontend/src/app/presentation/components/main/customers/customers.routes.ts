import { Routes } from "@angular/router";
import { CustomersListPage } from "./pages/customers-list-page/customers-list-page";
import { CustomersEditPage } from "./pages/customers-edit-page/customers-edit-page";
import { CustomersAddPage } from "./pages/customers-add-page/customers-add-page";

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