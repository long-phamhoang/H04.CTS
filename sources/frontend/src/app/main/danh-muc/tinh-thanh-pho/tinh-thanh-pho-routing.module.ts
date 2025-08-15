import { authGuard, permissionGuard } from '@abp/ng.core';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { TinhThanhPhoComponent } from './tinh-thanh-pho.component';

const routes: Routes = [
  {
    path: '', component: TinhThanhPhoComponent,
    canActivate: [authGuard, permissionGuard],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class TinhThanhPhoRoutingModule { }
