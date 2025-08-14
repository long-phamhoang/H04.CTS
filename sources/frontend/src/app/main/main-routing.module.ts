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
        path: 'danh-muc/nguoi-tiep-nhan',
        loadChildren: () => import('./danh-muc/nguoi-tiep-nhan/nguoi-tiep-nhan.module').then(m => m.NguoiTiepNhanModule),
      },
      {
        path: 'danh-muc/noi-cap-cccd',
        loadChildren: () => import('./danh-muc/noi-cap-cccd/noi-cap-cccd.module').then(m => m.NoiCapCCCDModule),
      }
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class MainRoutingModule { }
