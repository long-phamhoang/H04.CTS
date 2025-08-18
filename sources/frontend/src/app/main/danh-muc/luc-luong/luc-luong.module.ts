import { NgModule } from '@angular/core'; 
import { SharedModule } from '@app/shared/shared.module';
import { NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';
import { LucLuongRoutingModule } from './luc-luong-routing.module';
import { LucLuongComponent } from './luc-luong.component';
import { ProgressSpinnerModule } from 'primeng/progressspinner';

@NgModule({
	declarations: [
		LucLuongComponent
	],
	imports: [
		LucLuongRoutingModule,
		SharedModule,
		NgbDatepickerModule,
		ProgressSpinnerModule,
	],
})
export class LucLuongModule { }


