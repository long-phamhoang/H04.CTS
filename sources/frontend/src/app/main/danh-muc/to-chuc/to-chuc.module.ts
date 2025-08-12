import { NgModule } from '@angular/core';
import { SharedModule } from '@app/shared/shared.module';
import { NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';
import { ToChucRoutingModule } from './to-chuc-routing.module';

import { ToChucComponent } from './to-chuc.component';

@NgModule({
  declarations: [
    ToChucComponent
  ],
  imports: [
    ToChucRoutingModule,
    SharedModule,
    NgbDatepickerModule,
  ],
})
export class ToChucModule { }
