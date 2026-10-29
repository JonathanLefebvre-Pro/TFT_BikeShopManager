import { Routes } from '@angular/router';
import { LoginPage } from './presentation/components/auth/login-page/login-page';
import { MainPage } from './presentation/components/main/main-page/main-page';
import { AuthGuard } from './appCore/guards/AuthGuard';
import { LoginGuard } from './appCore/guards/LoginGuard';

export const routes: Routes = [

    {
        path: '',
        component: LoginPage,
        title: 'Login',
        pathMatch: 'full',
        canActivate: [LoginGuard]
    },
    {
        path: 'main',
        component: MainPage,
        title: 'Main',
        loadChildren: () => import('@presentation/components/main/main.routes').then(m => m.routes),
        canActivate: [AuthGuard]
    }
];
