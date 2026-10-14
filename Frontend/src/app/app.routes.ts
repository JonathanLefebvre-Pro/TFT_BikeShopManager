import { Routes } from '@angular/router';
import { DisplayCustomers } from './presentation/customers/display-customers/display-customers';
import { DisplayEmployees } from './presentation/employees/display-employees/display-employees';
import { DisplayStock } from './presentation/stock/display-stock/display-stock';
import { DisplayShop } from './presentation/shop/display-shop/display-shop';

export const routes: Routes = [
    {
        path: '',
        component: DisplayCustomers,
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
