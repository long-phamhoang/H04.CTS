import { NgModule } from '@angular/core';
import { MangCTSRoutingModule } from './mang-he-thong-cap-cts-routing.module';
import { MangHeThongCapCTSComponent } from './mang-he-thong-cap-cts.component';
import { SharedModule } from '@app/shared/shared.module';

@NgModule({
  declarations: [
    MangHeThongCapCTSComponent
  ],
  imports: [
    MangCTSRoutingModule,
    SharedModule,
  ],
})
export class MangHeThongCapCTSModule { }
