import { authGuard, permissionGuard } from '@abp/ng.core';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { NoiCapCCCDComponent } from './noi-cap-cccd.component';

const routes: Routes = [
  {
    path: '', component: NoiCapCCCDComponent,
    canActivate: [authGuard, permissionGuard],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class NoiCapCCCDRoutingModule { }
