import { Routes } from '@angular/router';
import { LoginPage } from './presentation/components/auth/login-page/login-page';
import { MainPage } from './presentation/components/main/main-page/main-page';

export const routes: Routes = [

    {
        path: '',
        component: LoginPage,
        title: 'Login',
        pathMatch: 'full'
    },
    {
        path: 'main',
        component: MainPage,
        title: 'Main',
        loadChildren: () => import('@presentation/components/main/main.routes').then(m => m.routes)
    }
];
