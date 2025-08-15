import { NgModule } from '@angular/core';
import { SharedModule } from '@app/shared/shared.module';
import { NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';
import { ChucVuRoutingModule } from './chuc-vu-routing.module';
import { ChucVuComponent } from './chuc-vu.component';
import { MustUniqueChucVuMaDirective } from './chuc-vu.directive';


@NgModule({
  declarations: [
    ChucVuComponent,
    MustUniqueChucVuMaDirective,
  ],
  imports: [
    ChucVuRoutingModule,
    SharedModule,
    NgbDatepickerModule,
  ],
})
export class ChucVuModule { }
