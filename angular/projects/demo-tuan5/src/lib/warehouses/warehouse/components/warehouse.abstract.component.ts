import { Directive, OnInit, inject } from '@angular/core';

import { ListService, TrackByService } from '@abp/ng.core';

import type { WarehouseDto } from '../../../proxy/warehouses/models';
import { WarehouseViewService } from '../services/warehouse.service';
import { WarehouseDetailViewService } from '../services/warehouse-detail.service';

export const ChildTabDependencies = [];

export const ChildComponentDependencies = [];

@Directive({ standalone: true })
export abstract class AbstractWarehouseComponent implements OnInit {
  public readonly list = inject(ListService);
  public readonly track = inject(TrackByService);
  public readonly service = inject(WarehouseViewService);
  public readonly serviceDetail = inject(WarehouseDetailViewService);
  protected title = 'DemoTuan5::Warehouses';

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

  update(record: WarehouseDto) {
    this.serviceDetail.update(record);
  }

  delete(record: WarehouseDto) {
    this.service.delete(record);
  }

  exportToExcel() {
    this.service.exportToExcel();
  }
}
