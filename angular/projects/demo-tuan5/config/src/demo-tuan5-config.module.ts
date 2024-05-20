import { ModuleWithProviders, NgModule } from '@angular/core';
import { DEMO_TUAN5_ROUTE_PROVIDERS } from './providers/route.provider';
import { COUNTRIES_COUNTRY_ROUTE_PROVIDER } from './providers/country-route.provider';
import { WAREHOUSES_WAREHOUSE_ROUTE_PROVIDER } from './providers/warehouse-route.provider';
import { WAREHOUSE_LOCATIONS_WAREHOUSE_LOCATION_ROUTE_PROVIDER } from './providers/warehouse-location-route.provider';

@NgModule()
export class DemoTuan5ConfigModule {
  static forRoot(): ModuleWithProviders<DemoTuan5ConfigModule> {
    return {
      ngModule: DemoTuan5ConfigModule,
      providers: [
        DEMO_TUAN5_ROUTE_PROVIDERS,
        COUNTRIES_COUNTRY_ROUTE_PROVIDER,
        WAREHOUSES_WAREHOUSE_ROUTE_PROVIDER,
        WAREHOUSE_LOCATIONS_WAREHOUSE_LOCATION_ROUTE_PROVIDER,
      ],
    };
  }
}
