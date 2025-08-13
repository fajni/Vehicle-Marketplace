import { Routes } from '@angular/router';
import { Login } from './login/login';
import { Registration } from './registration/registration';
import { Home } from './home/home';
import { Shop } from './shop/shop';
import { About } from './about/about';
import { ProtectedPage } from './protected-page/protected-page';

export const routes: Routes = [
    {
        path: '',
        component: Home
    },
    {
        path: 'home',
        redirectTo: '',
        pathMatch: 'full'
    },
    {
        path: 'shop',
        component: Shop
    },
    {
        path: 'about',
        component: About
    },
    {
        path: 'login',
        component: Login
    },
    // {
    //     path: 'logout',

    // },
    {
        path: 'registration',
        component: Registration
    },
    {
        path: 'protected',
        component: ProtectedPage
    }
    // {
    //     path: 'logout'
    // },
    // {
    //     path: 'account'
    // }
];
