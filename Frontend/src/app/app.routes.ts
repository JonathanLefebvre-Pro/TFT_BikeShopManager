import { Routes } from '@angular/router';
import { CustomersPage } from '@presentation/components/customers/customers-page/customers-page';
import { DisplayEmployees } from '@presentation/components/employees/display-employees/display-employees';
import { DisplayStock } from '@presentation/components/stock/display-stock/display-stock';
import { DisplayShop } from '@presentation/components/shop/display-shop/display-shop';
import { CustomersListPage } from '@presentation/components/customers/customers-list-page/customers-list-page';
import { CustomersEditPage } from '@presentation/components/customers/customers-edit-page/customers-edit-page';
import { CustomersAddPage } from '@presentation/components/customers/customers-add-page/customers-add-page';

export const routes: Routes = [

    {
        path: 'customers',
        component: CustomersPage,
        title: 'Customers',
        children: [
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
    },
    {
        path: '',
        redirectTo: '/customers/list',
        pathMatch: 'full'
    },
    {
        path: '**',
        redirectTo: ''
    }
];
