import { Injectable, inject } from '@angular/core';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import { ABP, downloadBlob, ListService, PagedResultDto } from '@abp/ng.core';
import { filter, switchMap, finalize } from 'rxjs/operators';
import type {
  GetWarehouseLocationsInput,
  WarehouseLocationWithNavigationPropertiesDto,
} from '../../../proxy/warehouse-locations/models';
import { WarehouseLocationService } from '../../../proxy/warehouse-locations/warehouse-location.service';

export abstract class AbstractWarehouseLocationViewService {
  protected readonly proxyService = inject(WarehouseLocationService);
  protected readonly confirmationService = inject(ConfirmationService);
  protected readonly list = inject(ListService);

  isExportToExcelBusy = false;

  data: PagedResultDto<WarehouseLocationWithNavigationPropertiesDto> = {
    items: [],
    totalCount: 0,
  };

  filters = {} as GetWarehouseLocationsInput;

  delete(record: WarehouseLocationWithNavigationPropertiesDto) {
    this.confirmationService
      .warn('DemoTuan5::DeleteConfirmationMessage', 'DemoTuan5::AreYouSure', {
        messageLocalizationParams: [],
      })
      .pipe(
        filter(status => status === Confirmation.Status.confirm),
        switchMap(() => this.proxyService.delete(record.warehouseLocation.id))
      )
      .subscribe(this.list.get);
  }

  hookToQuery() {
    const getData = (query: ABP.PageQueryParams) =>
      this.proxyService.getList({
        ...query,
        ...this.filters,
        filterText: query.filter,
      });

    const setData = (list: PagedResultDto<WarehouseLocationWithNavigationPropertiesDto>) =>
      (this.data = list);

    this.list.hookToQuery(getData).subscribe(setData);
  }

  clearFilters() {
    this.filters = {} as GetWarehouseLocationsInput;
    this.list.get();
  }

  exportToExcel() {
    this.isExportToExcelBusy = true;
    this.proxyService
      .getDownloadToken()
      .pipe(
        switchMap(({ token }) =>
          this.proxyService.getListAsExcelFile({
            downloadToken: token,
            filterText: this.list.filter,
            ...this.filters,
          })
        ),
        finalize(() => (this.isExportToExcelBusy = false))
      )
      .subscribe(result => {
        downloadBlob(result, 'WarehouseLocation.xlsx');
      });
  }
}
