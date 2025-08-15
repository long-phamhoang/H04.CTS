import { NgModule } from '@angular/core';
import { SharedModule } from '@app/shared/shared.module';
import { NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';
import { ThueBaoCaNhanComponent } from './thue-bao-ca-nhan.component';
import { ThueBaoCaNhanRoutingModule } from './thue-bao-ca-nhan-routing.module';


@NgModule({
  declarations: [
    ThueBaoCaNhanComponent,
  ],
  imports: [
    ThueBaoCaNhanRoutingModule,
    SharedModule,
    NgbDatepickerModule,
  ],
})
export class ThueBaoCaNhanModule { }
