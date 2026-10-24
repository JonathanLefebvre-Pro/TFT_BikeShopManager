import { Routes } from '@angular/router';
import { CustomersPage } from '@presentation/components/customers/customers-page/customers-page';
import { EmployeesPage } from '@/app/presentation/components/employees/employees-page/employees-page';
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
        component: EmployeesPage,
        title: 'Employees',
        loadChildren: () => import('@presentation/components/employees/employees.route').then(m => m.routes)
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
