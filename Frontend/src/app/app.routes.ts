import { Routes } from '@angular/router';
import { DisplayEmployees } from './presentation/components/employees/display-employees/display-employees';
import { DisplayStock } from './presentation/components/stock/display-stock/display-stock';
import { DisplayShop } from './presentation/components/shop/display-shop/display-shop';
import { CustomersPage } from './presentation/components/customers/customers-page/customers-page';

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
