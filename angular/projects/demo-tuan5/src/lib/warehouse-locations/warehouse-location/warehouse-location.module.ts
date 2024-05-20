import { NgModule } from '@angular/core';
import { WarehouseLocationComponent } from './components/warehouse-location.component';
import { WarehouseLocationRoutingModule } from './warehouse-location-routing.module';

@NgModule({
  declarations: [],
  imports: [WarehouseLocationComponent, WarehouseLocationRoutingModule],
})
export class WarehouseLocationModule {}

export function loadWarehouseLocationModuleAsChild() {
  return Promise.resolve(WarehouseLocationModule);
}
