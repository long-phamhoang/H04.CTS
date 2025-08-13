import { authGuard, permissionGuard } from '@abp/ng.core';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MangHeThongCapCTSComponent } from './mang-he-thong-cap-cts.component';

const routes: Routes = [
  {
    path: '', component: MangHeThongCapCTSComponent,
    canActivate: [authGuard, permissionGuard],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class MangCTSRoutingModule { }
