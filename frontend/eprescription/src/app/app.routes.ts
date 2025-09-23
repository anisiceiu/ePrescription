import { Routes } from '@angular/router';

export const APP_ROUTES: Routes = [
    {
    path: 'login',
    loadChildren: () => import('./features/auth/login.routes').then(m => m.LOGIN_ROUTES),
  },
  {
    path: 'dashboard',
    loadChildren: () => import('./features/dashboard/dashboard.routes').then(m => m.DASHBOARD_ROUTES),
  },
  { path: '', redirectTo: 'login', pathMatch: 'full' },
];
