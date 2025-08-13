import { authGuard, permissionGuard } from '@abp/ng.core';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { LucLuongComponent } from './luc-luong.component';

const routes: Routes = [
	{
		path: '', component: LucLuongComponent,
		canActivate: [authGuard, permissionGuard],
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class LucLuongRoutingModule { }


