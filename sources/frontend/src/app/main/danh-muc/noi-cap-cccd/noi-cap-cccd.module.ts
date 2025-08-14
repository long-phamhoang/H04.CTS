import { NgModule } from '@angular/core';
import { SharedModule } from '@app/shared/shared.module';
import { NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';
import { NoiCapCCCDRoutingModule } from './noi-cap-cccd-routing.module';

import { NoiCapCCCDComponent } from './noi-cap-cccd.component';

@NgModule({
  declarations: [
    NoiCapCCCDComponent
  ],
  imports: [
    NoiCapCCCDRoutingModule,
    SharedModule,
    NgbDatepickerModule,
  ],
})
export class NoiCapCCCDModule { }
