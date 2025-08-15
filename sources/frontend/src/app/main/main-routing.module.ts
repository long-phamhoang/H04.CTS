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
        path: 'danh-muc/loai-thiet-bi-dich-vu-phan-mem',
        loadChildren: () => import('./danh-muc/loai-thiet-bi-dich-vu-phan-mem/loai-thiet-bi-dich-vu-phan-mem.module')
        .then(m => m.LoaiThietBiDichVuPhanMemModule),
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class MainRoutingModule { }
