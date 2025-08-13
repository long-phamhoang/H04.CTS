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
        path: 'danh-muc/trich-yeu',
        loadChildren: () => import('./danh-muc/trich-yeu/trich-yeu.module').then(m => m.TrichYeuModule),
      },
      {
        path: 'danh-muc/mang-he-thong-cap-cts',
        loadChildren: () => import('./danh-muc/mang-he-thong-cap-cts/mang-he-thong-cap-cts.module').then(m => m.MangHeThongCapCTSModule)
      }
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class MainRoutingModule { }
