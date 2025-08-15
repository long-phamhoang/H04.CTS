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
        path: 'danh-muc/chuc-vu',
        loadChildren: () => import('./danh-muc/chuc-vu/chuc-vu.module').then(m => m.ChucVuModule),
      },
      {
        path: 'danh-muc/thue-bao-ca-nhan',
        loadChildren: () => import('./danh-muc/thue-bao-ca-nhan/thue-bao-ca-nhan.module').then(m => m.ThueBaoCaNhanModule),
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class MainRoutingModule { }
