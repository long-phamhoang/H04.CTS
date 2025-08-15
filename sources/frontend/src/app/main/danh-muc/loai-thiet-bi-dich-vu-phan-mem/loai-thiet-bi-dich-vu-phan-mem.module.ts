import { LoaiThietBiDichVuPhanMemRoutingModule } from "./loai-thiet-bi-dich-vu-phan-mem-routing.module";
import { LoaiThietBiDichVuPhanMemComponent } from "./loai-thiet-bi-dich-vu-phan-mem.component";
import { NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';
import { NgModule } from '@angular/core';
import { SharedModule } from '@app/shared/shared.module';

@NgModule({
    declarations: [
        LoaiThietBiDichVuPhanMemComponent
    ],
    imports: [
        LoaiThietBiDichVuPhanMemRoutingModule,
        SharedModule,
        NgbDatepickerModule
    ],
})
export class LoaiThietBiDichVuPhanMemModule {}