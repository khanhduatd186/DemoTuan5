import { inject, Injectable } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ListService, TrackByService } from '@abp/ng.core';
import { finalize, tap } from 'rxjs/operators';

import type { WarehouseLocationWithNavigationPropertiesDto } from '../../../proxy/warehouse-locations/models';
import { WarehouseLocationService } from '../../../proxy/warehouse-locations/warehouse-location.service';

export abstract class AbstractWarehouseLocationDetailViewService {
  protected readonly fb = inject(FormBuilder);
  protected readonly track = inject(TrackByService);
  public readonly proxyService = inject(WarehouseLocationService);
  public readonly list = inject(ListService);

  public readonly getCountryLookup = this.proxyService.getCountryLookup;

  public readonly getWarehouseLookup = this.proxyService.getWarehouseLookup;

  isBusy = false;
  isVisible = false;
  selected = {} as any;
  form: FormGroup | undefined;

  buildForm() {
    const { code, description, active, idx, countryId, warehouseId } =
      this.selected?.warehouseLocation || {};

    this.form = this.fb.group({
      code: [code ?? null, [Validators.required]],
      description: [description ?? null, []],
      active: [active ?? false, []],
      idx: [idx ?? null, []],
      countryId: [countryId ?? null, []],
      warehouseId: [warehouseId ?? null, []],
    });
  }

  showForm() {
    this.buildForm();
    this.isVisible = true;
  }

  create() {
    this.selected = undefined;
    this.showForm();
  }

  update(record: WarehouseLocationWithNavigationPropertiesDto) {
    this.selected = record;
    this.showForm();
  }

  hideForm() {
    this.isVisible = false;
    this.form.reset();
  }

  submitForm() {
    if (this.form.invalid) return;

    this.isBusy = true;

    const request = this.createRequest().pipe(
      finalize(() => (this.isBusy = false)),
      tap(() => this.hideForm())
    );

    request.subscribe(this.list.get);
  }

  private createRequest() {
    if (this.selected) {
      return this.proxyService.update(this.selected.warehouseLocation.id, {
        ...this.form.value,
        concurrencyStamp: this.selected.warehouseLocation.concurrencyStamp,
      });
    }
    return this.proxyService.create(this.form.value);
  }

  changeVisible($event: boolean) {
    this.isVisible = $event;
  }
}
