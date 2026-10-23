import { Routes } from '@angular/router';
import { CustomersPage } from '@presentation/components/customers/customers-page/customers-page';
import { DisplayEmployees } from '@presentation/components/employees/display-employees/display-employees';
import { DisplayStock } from '@presentation/components/stock/display-stock/display-stock';
import { DisplayShop } from '@presentation/components/shop/display-shop/display-shop';

export const routes: Routes = [

    {
        path: 'customers',
        component: CustomersPage,
        title: 'Customers',
        loadChildren: () => import('@presentation/components/customers/customers.route').then(m => m.routes)
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
