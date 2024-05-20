import { Directive, OnInit, inject } from '@angular/core';

import { ListService, TrackByService } from '@abp/ng.core';

import type { WarehouseLocationWithNavigationPropertiesDto } from '../../../proxy/warehouse-locations/models';
import { WarehouseLocationViewService } from '../services/warehouse-location.service';
import { WarehouseLocationDetailViewService } from '../services/warehouse-location-detail.service';

export const ChildTabDependencies = [];

export const ChildComponentDependencies = [];

@Directive({ standalone: true })
export abstract class AbstractWarehouseLocationComponent implements OnInit {
  public readonly list = inject(ListService);
  public readonly track = inject(TrackByService);
  public readonly service = inject(WarehouseLocationViewService);
  public readonly serviceDetail = inject(WarehouseLocationDetailViewService);
  protected title = 'DemoTuan5::WarehouseLocations';

  ngOnInit() {
    this.service.hookToQuery();
  }

  clearFilters() {
    this.service.clearFilters();
  }

  showForm() {
    this.serviceDetail.showForm();
  }

  create() {
    this.serviceDetail.selected = undefined;
    this.serviceDetail.showForm();
  }

  update(record: WarehouseLocationWithNavigationPropertiesDto) {
    this.serviceDetail.update(record);
  }

  delete(record: WarehouseLocationWithNavigationPropertiesDto) {
    this.service.delete(record);
  }

  exportToExcel() {
    this.service.exportToExcel();
  }
}
