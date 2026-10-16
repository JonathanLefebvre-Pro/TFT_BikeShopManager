import { Routes } from '@angular/router';
import { CustomersPage } from '@presentation/components/customers/customers-page/customers-page';
import { DisplayEmployees } from '@presentation/components/employees/display-employees/display-employees';
import { DisplayStock } from '@presentation/components/stock/display-stock/display-stock';
import { DisplayShop } from '@presentation/components/shop/display-shop/display-shop';
import { CustomersEditPage } from '@presentation/components/customers/customers-edit-page/customers-edit-page';
import { CustomersAddPage } from './presentation/components/customers/customers-add-page/customers-add-page';

export const routes: Routes = [
    {
        path: '',
        component: CustomersPage,
        title: 'Customers',
        children: [
            {
                path: 'edit',
                component: CustomersEditPage,
            },
            {
                path: 'add',
                component: CustomersAddPage,
            },
        ]
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
