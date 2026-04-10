import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountListComponent } from './components/account-list/account-list.component';
import { AccountFormComponent } from './components/account-form/account-form.component';

const routes: Routes = [
  { path: '', component: AccountListComponent },
  { path: 'add', component: AccountFormComponent },
  { path: 'edit/:id', component: AccountFormComponent },
  { path: ':id', component: AccountFormComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FinancialRoutingModule { }
