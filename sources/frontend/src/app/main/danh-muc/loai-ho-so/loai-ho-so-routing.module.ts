import { authGuard, permissionGuard } from "@abp/ng.core";
import { LoaiHoSoComponent } from "./loai-ho-so.component";
import { Routes, RouterModule } from '@angular/router';
import { NgModule } from "@angular/core";


const routes: Routes = [
    {
        path: '',
        component: LoaiHoSoComponent,
        canActivate: [authGuard, permissionGuard]
    }
]

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})

export class LoaiHoSoRoutingModule { }