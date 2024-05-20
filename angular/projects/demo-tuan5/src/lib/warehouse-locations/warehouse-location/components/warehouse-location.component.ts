import { ChangeDetectionStrategy, Component } from '@angular/core';
import {
  NgbDateAdapter,
  NgbCollapseModule,
  NgbDatepickerModule,
  NgbDropdownModule,
} from '@ng-bootstrap/ng-bootstrap';
import { NgxValidateCoreModule } from '@ngx-validate/core';
import { ListService, CoreModule } from '@abp/ng.core';
import { ThemeSharedModule, DateAdapter } from '@abp/ng.theme.shared';
import { PageModule } from '@abp/ng.components/page';
import { CommercialUiModule } from '@volo/abp.commercial.ng.ui';

import { WarehouseLocationViewService } from '../services/warehouse-location.service';
import { WarehouseLocationDetailViewService } from '../services/warehouse-location-detail.service';
import { WarehouseLocationDetailModalComponent } from './warehouse-location-detail.component';
import {
  AbstractWarehouseLocationComponent,
  ChildTabDependencies,
  ChildComponentDependencies,
} from './warehouse-location.abstract.component';

@Component({
  selector: 'lib-warehouse-location',
  changeDetection: ChangeDetectionStrategy.Default,
  standalone: true,
  imports: [
    ...ChildTabDependencies,
    NgbCollapseModule,
    NgbDatepickerModule,
    NgbDropdownModule,

    NgxValidateCoreModule,

    PageModule,
    CoreModule,
    ThemeSharedModule,
    CommercialUiModule,
    WarehouseLocationDetailModalComponent,
    ...ChildComponentDependencies,
  ],
  providers: [
    ListService,
    WarehouseLocationViewService,
    WarehouseLocationDetailViewService,
    { provide: NgbDateAdapter, useClass: DateAdapter },
  ],
  templateUrl: './warehouse-location.component.html',
  styles: `::ng-deep.datatable-row-detail { background: transparent !important; }`,
})
export class WarehouseLocationComponent extends AbstractWarehouseLocationComponent {}
