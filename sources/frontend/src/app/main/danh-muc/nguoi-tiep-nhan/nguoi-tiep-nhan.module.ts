import { NgModule } from '@angular/core';
import { SharedModule } from '@app/shared/shared.module';
import { NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';
import { NguoiTiepNhanRoutingModule } from './nguoi-tiep-nhan-routing.module';

import { NguoiTiepNhanComponent } from './nguoi-tiep-nhan.component';

@NgModule({
  declarations: [
    NguoiTiepNhanComponent
  ],
  imports: [
    NguoiTiepNhanRoutingModule,
    SharedModule,
    NgbDatepickerModule,
  ],
})
export class NguoiTiepNhanModule { }
