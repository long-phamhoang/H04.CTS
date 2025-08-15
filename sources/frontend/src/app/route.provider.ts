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
      path: '/main/danh-muc/tinh-thanh-pho',
      name: '::Menu:TinhThanhPho',
      iconClass: 'fas fa-book',
      order: 2,
    },
    {
      path: '/main/danh-muc/xa-phuong',
      name: '::Menu:XaPhuong',
      iconClass: 'fas fa-book',
      order: 2,
    },
  ]);
}
