import { NgModule } from '@angular/core';
import { WarehouseComponent } from './components/warehouse.component';
import { WarehouseRoutingModule } from './warehouse-routing.module';

@NgModule({
  declarations: [],
  imports: [WarehouseComponent, WarehouseRoutingModule],
})
export class WarehouseModule {}

export function loadWarehouseModuleAsChild() {
  return Promise.resolve(WarehouseModule);
}
