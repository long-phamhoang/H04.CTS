import { authGuard, permissionGuard } from '@abp/ng.core';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { XaPhuongComponent } from './xa-phuong.component';

const routes: Routes = [
  {
    path: '', component: XaPhuongComponent,
    canActivate: [authGuard, permissionGuard],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class XaPhuongRoutingModule { }
