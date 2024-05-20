import { authGuard, permissionGuard } from '@abp/ng.core';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { WarehouseLocationComponent } from './components/warehouse-location.component';

export const routes: Routes = [
  {
    path: '',
    component: WarehouseLocationComponent,
    canActivate: [authGuard, permissionGuard],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class WarehouseLocationRoutingModule {}
