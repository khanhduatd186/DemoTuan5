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

import { CountryViewService } from '../services/country.service';
import { CountryDetailViewService } from '../services/country-detail.service';
import { CountryDetailModalComponent } from './country-detail.component';
import {
  AbstractCountryComponent,
  ChildTabDependencies,
  ChildComponentDependencies,
} from './country.abstract.component';

@Component({
  selector: 'lib-country',
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
    CountryDetailModalComponent,
    ...ChildComponentDependencies,
  ],
  providers: [
    ListService,
    CountryViewService,
    CountryDetailViewService,
    { provide: NgbDateAdapter, useClass: DateAdapter },
  ],
  templateUrl: './country.component.html',
  styles: `::ng-deep.datatable-row-detail { background: transparent !important; }`,
})
export class CountryComponent extends AbstractCountryComponent {}
