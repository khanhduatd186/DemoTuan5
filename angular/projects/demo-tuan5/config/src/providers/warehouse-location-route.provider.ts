import { eLayoutType, RoutesService } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';
import { eDemoTuan5RouteNames } from '../enums/route-names';

export const WAREHOUSE_LOCATIONS_WAREHOUSE_LOCATION_ROUTE_PROVIDER = [
  { provide: APP_INITIALIZER, useFactory: configureRoutes, deps: [RoutesService], multi: true },
];

function configureRoutes(routes: RoutesService) {
  return () => {
    routes.add([
      {
        path: '/demo-tuan5/warehouse-locations',
        parentName: eDemoTuan5RouteNames.DemoTuan5,
        name: 'DemoTuan5::Menu:WarehouseLocations',
        layout: eLayoutType.application,
        requiredPolicy: 'DemoTuan5.WarehouseLocations',
      },
    ]);
  };
}
