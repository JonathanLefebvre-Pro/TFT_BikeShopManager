import { Routes } from '@angular/router';
import { DisplayEmployees } from './presentation/employees/display-employees/display-employees';
import { DisplayStock } from './presentation/stock/display-stock/display-stock';
import { DisplayShop } from './presentation/shop/display-shop/display-shop';
import { CustomersPage } from './presentation/customers/customers-page/customers-page';

export const routes: Routes = [
    {
        path: '',
        component: CustomersPage,
        title: 'Customers',
    },
    {
        path: 'employees',
        component: DisplayEmployees,
        title: 'Employees',
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
    }
];
