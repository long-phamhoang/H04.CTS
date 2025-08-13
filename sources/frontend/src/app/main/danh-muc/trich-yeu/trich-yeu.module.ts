import { NgModule } from "@angular/core";
import { TrichYeuComponent } from "./trich-yeu.component";
import { TrichYeuRoutingModule } from "./trich-yeu-routing.module";
import { SharedModule } from "@app/shared/shared.module";

@NgModule({
  declarations: [
    TrichYeuComponent
  ],
  imports: [
    TrichYeuRoutingModule,
    SharedModule,
  ],
})
export class TrichYeuModule { }