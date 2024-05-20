import { CoreModule } from '@abp/ng.core';
import { ThemeSharedModule, DateAdapter } from '@abp/ng.theme.shared';
import { CommercialUiModule } from '@volo/abp.commercial.ng.ui';
import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { NgbDateAdapter, NgbDatepickerModule, NgbNavModule } from '@ng-bootstrap/ng-bootstrap';
import { WarehouseLocationDetailViewService } from '../services/warehouse-location-detail.service';

@Component({
  selector: 'lib-warehouse-location-detail-modal',
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
  templateUrl: './warehouse-location-detail.component.html',
  styles: [],
})
export class WarehouseLocationDetailModalComponent {
  public readonly service = inject(WarehouseLocationDetailViewService);
}
