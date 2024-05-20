import { NgModule, NgModuleFactory, ModuleWithProviders } from '@angular/core';
import { CoreModule, LazyModuleFactory } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { DemoTuan5Component } from './components/demo-tuan5.component';
import { DemoTuan5RoutingModule } from './demo-tuan5-routing.module';

@NgModule({
  declarations: [DemoTuan5Component],
  imports: [CoreModule, ThemeSharedModule, DemoTuan5RoutingModule],
  exports: [DemoTuan5Component],
})
export class DemoTuan5Module {
  static forChild(): ModuleWithProviders<DemoTuan5Module> {
    return {
      ngModule: DemoTuan5Module,
      providers: [],
    };
  }

  static forLazy(): NgModuleFactory<DemoTuan5Module> {
    return new LazyModuleFactory(DemoTuan5Module.forChild());
  }
}
