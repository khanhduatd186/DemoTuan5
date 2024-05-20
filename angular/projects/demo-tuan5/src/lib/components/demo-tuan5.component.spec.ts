import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { DemoTuan5Component } from './demo-tuan5.component';

describe('DemoTuan5Component', () => {
  let component: DemoTuan5Component;
  let fixture: ComponentFixture<DemoTuan5Component>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ DemoTuan5Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DemoTuan5Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
