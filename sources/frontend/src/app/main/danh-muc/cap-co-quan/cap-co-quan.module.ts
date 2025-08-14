import { NgModule } from '@angular/core';
import { SharedModule } from '@app/shared/shared.module';
import { NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';
import { CapCoQuanRoutingModule } from './cap-co-quan-routing.module';

import { CapCoQuanComponent } from './cap-co-quan.component';

@NgModule({
  declarations: [
    CapCoQuanComponent
  ],
  imports: [
    CapCoQuanRoutingModule,
    SharedModule,
    NgbDatepickerModule,
  ],
})
export class CapCoQuanModule { }
