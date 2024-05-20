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

import { WarehouseViewService } from '../services/warehouse.service';
import { WarehouseDetailViewService } from '../services/warehouse-detail.service';
import { WarehouseDetailModalComponent } from './warehouse-detail.component';
import {
  AbstractWarehouseComponent,
  ChildTabDependencies,
  ChildComponentDependencies,
} from './warehouse.abstract.component';

@Component({
  selector: 'lib-warehouse',
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
    WarehouseDetailModalComponent,
    ...ChildComponentDependencies,
  ],
  providers: [
    ListService,
    WarehouseViewService,
    WarehouseDetailViewService,
    { provide: NgbDateAdapter, useClass: DateAdapter },
  ],
  templateUrl: './warehouse.component.html',
  styles: `::ng-deep.datatable-row-detail { background: transparent !important; }`,
})
export class WarehouseComponent extends AbstractWarehouseComponent {}
