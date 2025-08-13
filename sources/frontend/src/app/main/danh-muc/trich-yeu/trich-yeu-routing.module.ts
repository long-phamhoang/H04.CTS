import { authGuard, permissionGuard } from '@abp/ng.core';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TrichYeuComponent } from './trich-yeu.component';

const routes: Routes = [
  {
    path: '', component: TrichYeuComponent,
    canActivate: [authGuard, permissionGuard],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class TrichYeuRoutingModule { }
