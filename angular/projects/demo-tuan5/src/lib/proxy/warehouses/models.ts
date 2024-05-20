import type { AuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface GetWarehousesInput extends PagedAndSortedResultRequestDto {
  filterText?: string;
  code?: string;
  description?: string;
  active?: boolean;
}

export interface WarehouseCreateDto {
  code: string;
  description?: string;
  active?: boolean;
}

export interface WarehouseDto extends AuditedEntityDto<string> {
  code: string;
  description?: string;
  active?: boolean;
  concurrencyStamp?: string;
}

export interface WarehouseExcelDownloadDto {
  downloadToken?: string;
  filterText?: string;
  name?: string;
}

export interface WarehouseUpdateDto {
  code: string;
  description?: string;
  active?: boolean;
  concurrencyStamp?: string;
}
