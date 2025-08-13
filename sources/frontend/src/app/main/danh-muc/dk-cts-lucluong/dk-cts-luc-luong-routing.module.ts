import { authGuard, permissionGuard } from '@abp/ng.core';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { DKCtsLucLuongComponent } from './dk-cts-luc-luong.component';

const routes: Routes = [
	{
		path: '', component: DKCtsLucLuongComponent,
		canActivate: [authGuard, permissionGuard],
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class DKCtsLucLuongRoutingModule { }


