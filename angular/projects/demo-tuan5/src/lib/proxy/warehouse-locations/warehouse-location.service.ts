import type {
  GetWarehouseLocationsInput,
  WarehouseLocationCreateDto,
  WarehouseLocationDto,
  WarehouseLocationExcelDownloadDto,
  WarehouseLocationUpdateDto,
  WarehouseLocationWithNavigationPropertiesDto,
} from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { DownloadTokenResultDto, LookupDto, LookupRequestDto } from '../shared/models';

@Injectable({
  providedIn: 'root',
})
export class WarehouseLocationService {
  apiName = 'DemoTuan5';

  create = (input: WarehouseLocationCreateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, WarehouseLocationDto>(
      {
        method: 'POST',
        url: '/api/demo-tuan5/warehouse-locations',
        body: input,
      },
      { apiName: this.apiName, ...config }
    );

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>(
      {
        method: 'DELETE',
        url: `/api/demo-tuan5/warehouse-locations/${id}`,
      },
      { apiName: this.apiName, ...config }
    );

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, WarehouseLocationDto>(
      {
        method: 'GET',
        url: `/api/demo-tuan5/warehouse-locations/${id}`,
      },
      { apiName: this.apiName, ...config }
    );

  getCountryLookup = (input: LookupRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<LookupDto<string>>>(
      {
        method: 'GET',
        url: '/api/demo-tuan5/warehouse-locations/country-lookup',
        params: {
          filter: input.filter,
          skipCount: input.skipCount,
          maxResultCount: input.maxResultCount,
        },
      },
      { apiName: this.apiName, ...config }
    );

  getDownloadToken = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, DownloadTokenResultDto>(
      {
        method: 'GET',
        url: '/api/demo-tuan5/warehouse-locations/download-token',
      },
      { apiName: this.apiName, ...config }
    );

  getList = (input: GetWarehouseLocationsInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<WarehouseLocationWithNavigationPropertiesDto>>(
      {
        method: 'GET',
        url: '/api/demo-tuan5/warehouse-locations',
        params: {
          filterText: input.filterText,
          sorting: input.sorting,
          skipCount: input.skipCount,
          maxResultCount: input.maxResultCount,
          code: input.code,
          description: input.description,
          active: input.active,
          idxMin: input.idxMin,
          idxMax: input.idxMax,
          countryId: input.countryId,
          warehouseId: input.warehouseId,
        },
      },
      { apiName: this.apiName, ...config }
    );

  getListAsExcelFile = (input: WarehouseLocationExcelDownloadDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, Blob>(
      {
        method: 'GET',
        responseType: 'blob',
        url: '/api/demo-tuan5/warehouse-locations/as-excel-file',
        params: {
          downloadToken: input.downloadToken,
          filterText: input.filterText,
          name: input.name,
        },
      },
      { apiName: this.apiName, ...config }
    );

  getWarehouseLookup = (input: LookupRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<LookupDto<string>>>(
      {
        method: 'GET',
        url: '/api/demo-tuan5/warehouse-locations/warehouse-lookup',
        params: {
          filter: input.filter,
          skipCount: input.skipCount,
          maxResultCount: input.maxResultCount,
        },
      },
      { apiName: this.apiName, ...config }
    );

  getWithNavigationProperties = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, WarehouseLocationWithNavigationPropertiesDto>(
      {
        method: 'GET',
        url: `/api/demo-tuan5/warehouse-locations/with-navigation-properties/${id}`,
      },
      { apiName: this.apiName, ...config }
    );

  update = (id: string, input: WarehouseLocationUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, WarehouseLocationDto>(
      {
        method: 'PUT',
        url: `/api/demo-tuan5/warehouse-locations/${id}`,
        body: input,
      },
      { apiName: this.apiName, ...config }
    );

  constructor(private restService: RestService) {}
}
