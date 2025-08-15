import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoaiCTSComponent } from './loai-cts.component';
import { LoaiCTSRoutingModule } from './loai-cts-routing.module';
import { NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';
import { SharedModule } from '@app/shared/shared.module';


@NgModule({
  declarations: [
    LoaiCTSComponent
  ],
  imports: [
    LoaiCTSRoutingModule,
    SharedModule,
    NgbDatepickerModule,
    CommonModule
  ]
})
export class LoaiCTSModule { }
