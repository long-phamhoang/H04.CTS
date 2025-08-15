import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainComponent } from './main.component';

const routes: Routes = [
  {
    path: '',
    component: MainComponent,
    children: [
      {
        path: 'danh-muc/to-chuc',
        loadChildren: () => import('./danh-muc/to-chuc/to-chuc.module').then(m => m.ToChucModule),
      },
      {
        path: 'danh-muc/tinh-thanh-pho',
        loadChildren: () => import('./danh-muc/tinh-thanh-pho/tinh-thanh-pho.module').then(m => m.TinhThanhPhoModule),
      },
      {
        path: 'danh-muc/xa-phuong',
        loadChildren: () => import('./danh-muc/xa-phuong/xa-phuong.module').then(m => m.XaPhuongModule),
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class MainRoutingModule { }
