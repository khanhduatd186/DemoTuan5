import { Directive, OnInit, inject } from '@angular/core';

import { ListService, TrackByService } from '@abp/ng.core';

import type { CountryDto } from '../../../proxy/countries/models';
import { CountryViewService } from '../services/country.service';
import { CountryDetailViewService } from '../services/country-detail.service';

export const ChildTabDependencies = [];

export const ChildComponentDependencies = [];

@Directive({ standalone: true })
export abstract class AbstractCountryComponent implements OnInit {
  public readonly list = inject(ListService);
  public readonly track = inject(TrackByService);
  public readonly service = inject(CountryViewService);
  public readonly serviceDetail = inject(CountryDetailViewService);
  protected title = 'DemoTuan5::Countries';

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

  update(record: CountryDto) {
    this.serviceDetail.update(record);
  }

  delete(record: CountryDto) {
    this.service.delete(record);
  }

  exportToExcel() {
    this.service.exportToExcel();
  }
}
