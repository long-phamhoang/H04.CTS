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
      order: 2,
    },
    {
      path: '/main/danh-muc/luc-luong',
      name: '::Menu:DanhMucs:LucLuong',
      parentName: '::Menu:DanhMucs',
      iconClass: 'fas fa-book',
      order: 1,
      layout: eLayoutType.empty,
    },
    {
      path: '/main/danh-muc/dk-cts-luc-luong',
      name: '::Menu:DanhMucs:DKCtsLucLuong',
      parentName: '::Menu:DanhMucs',
      iconClass: 'fas fa-book',
      order: 3,
      layout: eLayoutType.empty,
    },
  ]);
}
