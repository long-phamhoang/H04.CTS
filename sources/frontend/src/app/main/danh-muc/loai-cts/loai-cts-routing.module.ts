import { authGuard, permissionGuard } from "@abp/ng.core";
import { RouterModule, Routes } from "@angular/router";
import { LoaiCTSComponent } from "./loai-cts.component";
import { NgModule } from "@angular/core";

const routes: Routes = [
    {
        path: '',
        component: LoaiCTSComponent,
        canActivate: [authGuard, permissionGuard],
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})

export class LoaiCTSRoutingModule { }