import { NgModule } from '@angular/core';
import { SharedModule } from '@app/shared/shared.module';
import { NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';
import { TinhThanhPhoRoutingModule } from './tinh-thanh-pho-routing.module';

import { TinhThanhPhoComponent } from './tinh-thanh-pho.component';
import { TableModule } from 'primeng/table';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';

@NgModule({
  declarations: [
    TinhThanhPhoComponent
  ],
  imports: [
    TinhThanhPhoRoutingModule,
    SharedModule,
    NgbDatepickerModule,
    TableModule,
    NgxDatatableModule,
  ],
})
export class TinhThanhPhoModule { }