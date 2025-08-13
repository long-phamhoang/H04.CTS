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
      path: '/main/danh-muc/mang-he-thong-cap-cts',
      name: '::Menu:MangHeThongCapCts',
      iconClass: 'fas fa-book',
      order: 9,
      layout: eLayoutType.empty
    },
    {
      path: '/main/danh-muc/trich-yeu',
      name: '::Menu:TrichYeu',
      iconClass: 'fas fa-book',
      order: 16,
      layout: eLayoutType.empty
    },
  ]);
}
