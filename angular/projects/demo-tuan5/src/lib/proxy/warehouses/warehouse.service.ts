import type {
  GetWarehousesInput,
  WarehouseCreateDto,
  WarehouseDto,
  WarehouseExcelDownloadDto,
  WarehouseUpdateDto,
} from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { DownloadTokenResultDto } from '../shared/models';

@Injectable({
  providedIn: 'root',
})
export class WarehouseService {
  apiName = 'DemoTuan5';

  create = (input: WarehouseCreateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, WarehouseDto>(
      {
        method: 'POST',
        url: '/api/demo-tuan5/warehouses',
        body: input,
      },
      { apiName: this.apiName, ...config }
    );

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>(
      {
        method: 'DELETE',
        url: `/api/demo-tuan5/warehouses/${id}`,
      },
      { apiName: this.apiName, ...config }
    );

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, WarehouseDto>(
      {
        method: 'GET',
        url: `/api/demo-tuan5/warehouses/${id}`,
      },
      { apiName: this.apiName, ...config }
    );

  getDownloadToken = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, DownloadTokenResultDto>(
      {
        method: 'GET',
        url: '/api/demo-tuan5/warehouses/download-token',
      },
      { apiName: this.apiName, ...config }
    );

  getList = (input: GetWarehousesInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<WarehouseDto>>(
      {
        method: 'GET',
        url: '/api/demo-tuan5/warehouses',
        params: {
          filterText: input.filterText,
          sorting: input.sorting,
          skipCount: input.skipCount,
          maxResultCount: input.maxResultCount,
          code: input.code,
          description: input.description,
          active: input.active,
        },
      },
      { apiName: this.apiName, ...config }
    );

  getListAsExcelFile = (input: WarehouseExcelDownloadDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, Blob>(
      {
        method: 'GET',
        responseType: 'blob',
        url: '/api/demo-tuan5/warehouses/as-excel-file',
        params: {
          downloadToken: input.downloadToken,
          filterText: input.filterText,
          name: input.name,
        },
      },
      { apiName: this.apiName, ...config }
    );

  update = (id: string, input: WarehouseUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, WarehouseDto>(
      {
        method: 'PUT',
        url: `/api/demo-tuan5/warehouses/${id}`,
        body: input,
      },
      { apiName: this.apiName, ...config }
    );

  constructor(private restService: RestService) {}
}
