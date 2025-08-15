import { Routes, RouterModule } from "@angular/router";
import { LoaiThietBiDichVuPhanMemComponent } from "./loai-thiet-bi-dich-vu-phan-mem.component";
import { authGuard, permissionGuard } from "@abp/ng.core";
import { NgModule } from "@angular/core";


const routes: Routes = [
    {
        path: '', component: LoaiThietBiDichVuPhanMemComponent,
        canActivate: [authGuard, permissionGuard],
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class LoaiThietBiDichVuPhanMemRoutingModule { }