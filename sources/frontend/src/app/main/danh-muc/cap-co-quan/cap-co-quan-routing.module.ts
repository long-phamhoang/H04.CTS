import { authGuard, permissionGuard } from '@abp/ng.core';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { CapCoQuanComponent } from './cap-co-quan.component';

const routes: Routes = [
  {
    path: '', component: CapCoQuanComponent,
    canActivate: [authGuard, permissionGuard],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CapCoQuanRoutingModule { }
