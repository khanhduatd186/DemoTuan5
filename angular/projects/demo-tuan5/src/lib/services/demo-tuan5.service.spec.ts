import { TestBed } from '@angular/core/testing';

import { DemoTuan5Service } from './demo-tuan5.service';

describe('DemoTuan5Service', () => {
  let service: DemoTuan5Service;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DemoTuan5Service);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
