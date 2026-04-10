import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BusinessActivityListComponent } from './components/business-activity-list/business-activity-list.component';
import { InsuranceCompanyListComponent } from './components/insurance-company-list/insurance-company-list.component';

const routes: Routes = [
  { path: '', redirectTo: 'business-activities', pathMatch: 'full' },
  { path: 'business-activities', component: BusinessActivityListComponent },
  { path: 'insurance-companies', component: InsuranceCompanyListComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MasterTableRoutingModule { }
