import type { AuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CountryCreateDto {
  code: string;
  description?: string;
}

export interface CountryDto extends AuditedEntityDto<string> {
  code: string;
  description?: string;
  concurrencyStamp?: string;
}

export interface CountryExcelDownloadDto {
  downloadToken?: string;
  filterText?: string;
  name?: string;
}

export interface CountryUpdateDto {
  code: string;
  description?: string;
  concurrencyStamp?: string;
}

export interface GetCountriesInput extends PagedAndSortedResultRequestDto {
  filterText?: string;
  code?: string;
  description?: string;
}
