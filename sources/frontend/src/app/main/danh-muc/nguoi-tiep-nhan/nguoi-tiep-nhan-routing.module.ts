import { authGuard, permissionGuard } from '@abp/ng.core';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { NguoiTiepNhanComponent } from './nguoi-tiep-nhan.component';

const routes: Routes = [
  {
    path: '', component: NguoiTiepNhanComponent,
    canActivate: [authGuard, permissionGuard],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class NguoiTiepNhanRoutingModule { }
