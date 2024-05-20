import { Injectable } from '@angular/core';
import { RestService } from '@abp/ng.core';

@Injectable({
  providedIn: 'root',
})
export class DemoTuan5Service {
  apiName = 'DemoTuan5';

  constructor(private restService: RestService) {}

  sample() {
    return this.restService.request<void, any>(
      { method: 'GET', url: '/api/DemoTuan5/sample' },
      { apiName: this.apiName }
    );
  }
}
