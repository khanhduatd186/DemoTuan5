import type {
  CountryCreateDto,
  CountryDto,
  CountryExcelDownloadDto,
  CountryUpdateDto,
  GetCountriesInput,
} from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { DownloadTokenResultDto } from '../shared/models';

@Injectable({
  providedIn: 'root',
})
export class CountryService {
  apiName = 'DemoTuan5';

  create = (input: CountryCreateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CountryDto>(
      {
        method: 'POST',
        url: '/api/demo-tuan5/countries',
        body: input,
      },
      { apiName: this.apiName, ...config }
    );

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>(
      {
        method: 'DELETE',
        url: `/api/demo-tuan5/countries/${id}`,
      },
      { apiName: this.apiName, ...config }
    );

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CountryDto>(
      {
        method: 'GET',
        url: `/api/demo-tuan5/countries/${id}`,
      },
      { apiName: this.apiName, ...config }
    );

  getDownloadToken = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, DownloadTokenResultDto>(
      {
        method: 'GET',
        url: '/api/demo-tuan5/countries/download-token',
      },
      { apiName: this.apiName, ...config }
    );

  getList = (input: GetCountriesInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<CountryDto>>(
      {
        method: 'GET',
        url: '/api/demo-tuan5/countries',
        params: {
          filterText: input.filterText,
          sorting: input.sorting,
          skipCount: input.skipCount,
          maxResultCount: input.maxResultCount,
          code: input.code,
          description: input.description,
        },
      },
      { apiName: this.apiName, ...config }
    );

  getListAsExcelFile = (input: CountryExcelDownloadDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, Blob>(
      {
        method: 'GET',
        responseType: 'blob',
        url: '/api/demo-tuan5/countries/as-excel-file',
        params: {
          downloadToken: input.downloadToken,
          filterText: input.filterText,
          name: input.name,
        },
      },
      { apiName: this.apiName, ...config }
    );

  update = (id: string, input: CountryUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CountryDto>(
      {
        method: 'PUT',
        url: `/api/demo-tuan5/countries/${id}`,
        body: input,
      },
      { apiName: this.apiName, ...config }
    );

  constructor(private restService: RestService) {}
}
