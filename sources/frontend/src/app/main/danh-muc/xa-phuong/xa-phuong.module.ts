import { NgModule } from '@angular/core';
import { SharedModule } from '@app/shared/shared.module';
import { NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';
import { XaPhuongRoutingModule } from './xa-phuong-routing.module';

import { XaPhuongComponent } from './xa-phuong.component';

@NgModule({
  declarations: [
    XaPhuongComponent
  ],
  imports: [
    XaPhuongRoutingModule,
    SharedModule,
    NgbDatepickerModule, 
  ],
})
export class XaPhuongModule { } 