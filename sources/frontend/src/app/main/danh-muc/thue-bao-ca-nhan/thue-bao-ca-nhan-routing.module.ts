import { authGuard, permissionGuard } from '@abp/ng.core';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ThueBaoCaNhanComponent } from './thue-bao-ca-nhan.component';




const routes: Routes = [
  {
    path: '', component: ThueBaoCaNhanComponent,
    canActivate: [authGuard, permissionGuard],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ThueBaoCaNhanRoutingModule { }
