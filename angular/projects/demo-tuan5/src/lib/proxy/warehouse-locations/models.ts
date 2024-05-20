import type { AuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';
import type { CountryDto } from '../countries/models';
import type { WarehouseDto } from '../warehouses/models';

export interface GetWarehouseLocationsInput extends PagedAndSortedResultRequestDto {
  filterText?: string;
  code?: string;
  description?: string;
  active?: boolean;
  idxMin?: number;
  idxMax?: number;
  countryId?: string;
  warehouseId?: string;
}

export interface WarehouseLocationCreateDto {
  code: string;
  description?: string;
  active?: boolean;
  idx?: number;
  countryId?: string;
  warehouseId?: string;
}

export interface WarehouseLocationDto extends AuditedEntityDto<string> {
  code: string;
  description?: string;
  active?: boolean;
  idx?: number;
  countryId?: string;
  warehouseId?: string;
  concurrencyStamp?: string;
}

export interface WarehouseLocationExcelDownloadDto {
  downloadToken?: string;
  filterText?: string;
  name?: string;
}

export interface WarehouseLocationUpdateDto {
  code: string;
  description?: string;
  active?: boolean;
  idx?: number;
  countryId?: string;
  warehouseId?: string;
  concurrencyStamp?: string;
}

export interface WarehouseLocationWithNavigationPropertiesDto {
  warehouseLocation: WarehouseLocationDto;
  country: CountryDto;
  warehouse: WarehouseDto;
}
