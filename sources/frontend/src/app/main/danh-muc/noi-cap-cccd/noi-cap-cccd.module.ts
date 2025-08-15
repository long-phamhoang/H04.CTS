import { NgModule } from '@angular/core';
import { SharedModule } from '@app/shared/shared.module';
import { NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';
import { ToggleSwitchModule } from 'primeng/toggleswitch';
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
    ToggleSwitchModule,
  ],
})
export class NoiCapCCCDModule { }
