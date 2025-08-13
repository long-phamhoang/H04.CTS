import { NgModule } from '@angular/core'; 
import { SharedModule } from '@app/shared/shared.module';
import { NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';
import { DKCtsLucLuongRoutingModule } from './dk-cts-luc-luong-routing.module';

import { DKCtsLucLuongComponent } from './dk-cts-luc-luong.component';

@NgModule({
	declarations: [
		DKCtsLucLuongComponent
	],
	imports: [
		DKCtsLucLuongRoutingModule,
		SharedModule,
		NgbDatepickerModule,
	],
})
export class DKCtsLucLuongModule { }


