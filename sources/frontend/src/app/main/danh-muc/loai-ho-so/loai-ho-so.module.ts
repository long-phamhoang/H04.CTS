import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '@app/shared/shared.module';
import { NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';
import { LoaiHoSoRoutingModule } from './loai-ho-so-routing.module';

import { LoaiHoSoComponent } from './loai-ho-so.component';
import { DEFAULT_VALIDATION_BLUEPRINTS, provideAbpThemeShared } from '@abp/ng.theme.shared';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    LoaiHoSoComponent,
  ],
  imports: [
    LoaiHoSoRoutingModule,
    CommonModule,
    SharedModule,
    NgbDatepickerModule,
    FormsModule
  ],
  providers: [
    {
      provide: DEFAULT_VALIDATION_BLUEPRINTS,
      useValue: {
        ...DEFAULT_VALIDATION_BLUEPRINTS,
        required: "::RequiredInput"
      }
    }
  ]
})

export class LoaiHoSoModule { }
