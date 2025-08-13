//#region Abp Modules
import { CoreModule } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
//#endregion
//#region Angular Core Modules
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
//#endregion
//#region Bootstrap Modules
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ModalModule } from 'ngx-bootstrap/modal';
import { PopoverModule } from 'ngx-bootstrap/popover';
import { TabsModule } from 'ngx-bootstrap/tabs';
//#endregion
//#region PrimeNg Modules
import { AutoCompleteModule } from 'primeng/autocomplete';
import { CalendarModule } from 'primeng/calendar';
import { CheckboxModule } from 'primeng/checkbox';
import { FieldsetModule } from 'primeng/fieldset';
import { FileUploadModule as PrimeNGFileUploadModule } from 'primeng/fileupload';
import { InputGroupAddonModule } from 'primeng/inputgroupaddon';
import { InputGroupModule } from 'primeng/inputgroup';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextModule } from 'primeng/inputtext';
import { MenuModule } from 'primeng/menu';
import { MultiSelectModule } from 'primeng/multiselect';
import { PaginatorModule } from 'primeng/paginator';
import { PanelMenuModule } from 'primeng/panelmenu';
import { PanelModule } from 'primeng/panel';  
import { DialogModule } from 'primeng/dialog';
import { ProgressBarModule } from 'primeng/progressbar';
import { RadioButtonModule } from 'primeng/radiobutton';
import { SliderModule } from 'primeng/slider';
import { TableModule } from 'primeng/table';
import { TabViewModule } from 'primeng/tabview';
import { TagModule } from 'primeng/tag';
import { TooltipModule } from 'primeng/tooltip';
//#endregion
//#region Extension Modules
import { NgbDropdownModule, NgbModalModule, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgxValidateCoreModule } from '@ngx-validate/core';
//#endregion

const modules = [
  // Abp Modules
  CoreModule,
  ThemeSharedModule,

  // Angular Forms
  ReactiveFormsModule,

  // PrimeNg Modules
  AutoCompleteModule,
  CalendarModule,
  CheckboxModule,
  FieldsetModule,
  PrimeNGFileUploadModule,
  InputGroupAddonModule,
  InputGroupModule,
  InputNumberModule,
  InputTextModule,
  MenuModule,
  MultiSelectModule,
  PaginatorModule,
  PanelMenuModule,
  PanelModule,
  DialogModule,
  ProgressBarModule,
  RadioButtonModule,
  SliderModule,
  TableModule,
  TabViewModule,
  TagModule,
  TooltipModule,

  // Extension Modules
  NgbDropdownModule,
  NgbModalModule,
  NgbModule,
  NgxValidateCoreModule
];

@NgModule({
  declarations: [

  ],
  imports: [
    ...modules,
  ],
  exports: [
    ...modules,
  ],
  providers: []
})
export class SharedModule { }
