import { Routes } from '@angular/router';
import { Login } from './login/login';
import { Registration } from './registration/registration';
import { Home } from './home/home';
import { Shop } from './shop/shop';
import { About } from './about/about';
import { MyAccount } from './my-account/my-account';
import { Cars } from './shop/cars/cars';
import { Motorcycles } from './shop/motorcycles/motorcycles';
import { Car } from './shop/cars/car/car';
import { Motorcycle } from './shop/motorcycles/motorcycle/motorcycle';

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
        path: 'about',
        component: About
    },
    {
        path: 'shop',
        component: Shop
    },
    {
        path: 'cars',
        component: Cars
    },
    {
        path: 'motorcycles',
        component: Motorcycles
    },
    {
        path: 'cars/:vin',
        component: Car
    },
    {
        path: 'motorcycles/:vin',
        component: Motorcycle
    },
    {
        path: 'motors/:vin',
        redirectTo: 'motorcycles/:vin'
    },
    {
        path: 'motors',
        redirectTo: 'motorcycles'
    },
    {
        path: 'login',
        component: Login
    },
    {
        path: 'registration',
        component: Registration
    },
    {
        path: 'account',
        component: MyAccount
    }
];
