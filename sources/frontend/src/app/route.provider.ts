import { RoutesService, eLayoutType } from '@abp/ng.core';
import { inject, provideAppInitializer } from '@angular/core';

export const APP_ROUTE_PROVIDER = [
  provideAppInitializer(() => {
    configureRoutes();
  }),
];

function configureRoutes() {
  const routes = inject(RoutesService);
  routes.add([
    {
      path: '/',
      name: '::Menu:Home',
      iconClass: 'fas fa-home',
      order: 1,
      layout: eLayoutType.application,
    },
    {
      path: '/main/danh-muc/to-chuc',
      name: '::Menu:DanhMucs',
      iconClass: 'fas fa-book',
      order: 2,
    },
    {
      path: '/main/danh-muc/chuc-vu',
      name: '::Menu:DanhMucs:ChucVu',
      parentName: '::Menu:DanhMucs',
      iconClass: 'fas fa-book',
      layout: eLayoutType.empty,
    },
    {
      path: '/main/danh-muc/thue-bao-ca-nhan',
      name: '::Menu:DanhMucs:ThueBaoCaNhan',
      parentName: '::Menu:DanhMucs',
      iconClass: 'fas fa-book',
      layout: eLayoutType.empty,
    },
  ]);
}
