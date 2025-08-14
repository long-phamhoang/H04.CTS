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
      layout: eLayoutType.empty,
      order: 2,
    },
    {
      path: '/main/danh-muc/nguoi-tiep-nhan',
      name: '::Menu:DanhMucs.NguoiTiepNhan',
      iconClass: 'fas fa-book',
      layout: eLayoutType.empty,
      order: 3,
    },
    {
      path: '/main/danh-muc/noi-cap-cccd',
      name: '::Menu:DanhMucs.NoiCapCCCD',
      iconClass: 'fas fa-book',
      layout: eLayoutType.empty,
      order: 4,
    },
  ]);
}
