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
      path: 'danh-muc/luc-luong',
      loadChildren: () => import('./danh-muc/luc-luong/luc-luong.module').then(m => m.LucLuongModule),
    },
    {
      path: 'danh-muc/dk-cts-luc-luong',
      loadChildren: () => import('./danh-muc/dk-cts-lucluong/dk-cts-luc-luong.module').then(m => m.DKCtsLucLuongModule),
    },
    ],
  },
];
  
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class MainRoutingModule { }
