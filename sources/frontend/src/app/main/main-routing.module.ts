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
        path: 'danh-muc/loai-chung-thu-so',
        loadChildren: () => import('./danh-muc/loai-cts/loai-cts.module').then(m => m.LoaiCTSModule),
      },
      {
        path: 'danh-muc/loai-ho-so',
        loadChildren: () => import('./danh-muc/loai-ho-so/loai-ho-so.module').then(m => m.LoaiHoSoModule),
      }
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class MainRoutingModule { }
