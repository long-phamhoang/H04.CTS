import { NgModule } from '@angular/core';
import { SharedModule } from '@app/shared/shared.module';
import { MainRoutingModule } from './main-routing.module';

import { MainComponent } from './main.component';
import { LoaiHoSoComponent } from './danh-muc/loai-ho-so/loai-ho-so.component';

@NgModule({
  declarations: [
    MainComponent,
  ],
  imports: [
    SharedModule,
    MainRoutingModule,
  ],
})
export class MainModule { }
