import { NgModule } from '@angular/core';
import { SharedModule } from '@app/shared/shared.module';
import { NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';
import { CapCoQuanRoutingModule } from './cap-co-quan-routing.module';
import { CapCoQuanComponent } from './cap-co-quan.component';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { PaginatorModule } from 'primeng/paginator';

@NgModule({
  declarations: [
    CapCoQuanComponent
  ],
  imports: [
    CapCoQuanRoutingModule,
    SharedModule,
    NgbDatepickerModule,
    NgxDatatableModule,
    PaginatorModule
  ],
})
export class CapCoQuanModule { }
