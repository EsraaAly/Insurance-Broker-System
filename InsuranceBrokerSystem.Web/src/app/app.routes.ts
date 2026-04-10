import { Routes } from '@angular/router';
import { LoginComponent } from './components/auth/login/login.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { MainLayoutComponent } from './components/layout/main-layout/main-layout.component';
import { authGuard } from './guards/auth.guard';

export const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  {
    path: '',
    component: MainLayoutComponent,
    canActivate: [authGuard],
    children: [
      { path: 'dashboard', component: DashboardComponent },
      {
        path: 'clients',
        loadChildren: () => import('./modules/clients/clients.module').then(m => m.ClientsModule)
      },
      {
        path: 'financial',
        loadChildren: () => import('./modules/financial/financial.module').then(m => m.FinancialModule)
      },
      {
        path: 'master-table',
        loadChildren: () => import('./modules/master-table/master-table.module').then(m => m.MasterTableModule)
      }
    ]
  },
  { path: '**', redirectTo: '/dashboard' }
];
