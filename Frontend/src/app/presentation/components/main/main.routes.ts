import { Routes } from "@angular/router";
import { CustomersPage } from "./customers/pages/customers-page/customers-page";
import { EmployeesPage } from "./employees/pages/employees-page/employees-page";
import { DisplayStock } from "./stock/display-stock/display-stock";
import { DisplayShop } from "./shop/display-shop/display-shop";

export const routes: Routes = [
    {
        path: 'customers',
        component: CustomersPage,
        title: 'Customers',
        loadChildren: () => import('@/app/presentation/components/main/customers/customers.routes').then(m => m.routes)
    },
    {
        path: 'employees',
        component: EmployeesPage,
        title: 'Employees',
        loadChildren: () => import('@/app/presentation/components/main/employees/employees.routes').then(m => m.routes)
    },
    {
        path: 'stock',
        component: DisplayStock,
        title: 'Stock',
    },
    {
        path: 'shop',
        component: DisplayShop,
        title: 'Shop',
    },
    {
        path: '',
        redirectTo: 'customers',
        pathMatch: 'full'
    }/*,
    {
        path: '**',
        redirectTo: ''
    }*/
];