import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DemoTuan5Component } from './components/demo-tuan5.component';
import { loadCountryModuleAsChild } from './countries/country/country.module';
import { loadWarehouseModuleAsChild } from './warehouses/warehouse/warehouse.module';
import { loadWarehouseLocationModuleAsChild } from './warehouse-locations/warehouse-location/warehouse-location.module';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: DemoTuan5Component,
  },
  { path: 'countries', loadChildren: loadCountryModuleAsChild },
  { path: 'warehouses', loadChildren: loadWarehouseModuleAsChild },
  { path: 'warehouse-locations', loadChildren: loadWarehouseLocationModuleAsChild },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class DemoTuan5RoutingModule {}
