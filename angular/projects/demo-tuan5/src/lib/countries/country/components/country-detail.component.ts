import { CoreModule } from '@abp/ng.core';
import { ThemeSharedModule, DateAdapter } from '@abp/ng.theme.shared';
import { CommercialUiModule } from '@volo/abp.commercial.ng.ui';
import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { NgbDateAdapter, NgbDatepickerModule, NgbNavModule } from '@ng-bootstrap/ng-bootstrap';
import { CountryDetailViewService } from '../services/country-detail.service';

@Component({
  selector: 'lib-country-detail-modal',
  changeDetection: ChangeDetectionStrategy.Default,
  standalone: true,
  imports: [
    CoreModule,
    ThemeSharedModule,
    CommercialUiModule,
    ReactiveFormsModule,
    NgbDatepickerModule,
    NgbNavModule,
  ],
  providers: [{ provide: NgbDateAdapter, useClass: DateAdapter }],
  templateUrl: './country-detail.component.html',
  styles: [],
})
export class CountryDetailModalComponent {
  public readonly service = inject(CountryDetailViewService);
}
