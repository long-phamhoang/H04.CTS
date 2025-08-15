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
      name: '::Menu:DanhMucs',
      iconClass: 'fas fa-book',
    },
    {
      path: '/main/danh-muc/to-chuc',
      name: '::Menu:ToChuc',
      parentName: '::Menu:DanhMucs',
      layout: eLayoutType.empty,
    },
    {
      path: '/main/danh-muc/loai-ho-so',
      name: '::Menu:LoaiHoSo',
      parentName: '::Menu:DanhMucs',
      layout: eLayoutType.empty,
    },
    {
      path: '/main/danh-muc/loai-chung-thu-so',
      name: '::Menu:LoaiCTS',
      parentName: '::Menu:DanhMucs',
      layout: eLayoutType.empty,
    },
  ]);
}
