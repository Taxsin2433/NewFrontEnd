// pages
import Home from "./pages/Home";
import About from "./pages/About";
import Products from "./pages/Products";
import ApiTest from "./pages/ApiTest";
import LoginPage from "./pages/LoginPage";
import RegistrationPage from "./pages/RegistrationPage";

// other
import {FC} from "react";



// interface
interface Route {
    key: string,
    title: string,
    path: string,
    enabled: boolean,
    component: FC<{}>
}

export const routes: Array<Route> = [
    {
        key: 'home-route',
        title: 'Home',
        path: '/',
        enabled: true,
        component: Home
    },
    {
        key: 'about-route',
        title: 'About',
        path: '/about',
        enabled: true,
        component: About
    },
    {
        key: 'products-route',
        title: 'Products',
        path: '/products',
        enabled: true,
        component: Products
    },
    {
        key: 'apitest-route',
        title: 'ApiTest',
        path: '/apitest',
        enabled: true,
        component: ApiTest
    },
    {
        key: 'login-route',
        title: 'Login',
        path: '/login',
        enabled: true,
        component: LoginPage
    },
    {
        key: 'Redistration-route',
        title: 'Registration',
        path: '/Registration',
        enabled: true,
        component: RegistrationPage
    }
]