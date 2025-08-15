//#region Abp Modules
import { CoreModule } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
//#endregion
//#region Angular Core Modules
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
//#endregion
//#region Bootstrap Modules
import {
  NgbDropdownModule,
  NgbToastModule,
} from '@ng-bootstrap/ng-bootstrap';
//#endregion
//#region PrimeNg Modules
import { AutoCompleteModule } from 'primeng/autocomplete';
import { CalendarModule } from 'primeng/calendar';
import { CheckboxModule } from 'primeng/checkbox';
import { DialogModule } from 'primeng/dialog';
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
import { ProgressBarModule } from 'primeng/progressbar';
import { RadioButtonModule } from 'primeng/radiobutton';
import { SliderModule } from 'primeng/slider';
import { TableModule } from 'primeng/table';
import { TabViewModule } from 'primeng/tabview';
import { TagModule } from 'primeng/tag';
import { TooltipModule } from 'primeng/tooltip';
import { InputSwitchModule } from 'primeng/inputswitch';
//#endregion
//#region Extension Modules
import { NgxValidateCoreModule } from '@ngx-validate/core';
//#endregion

//#region Components
import { PageHeaderComponent } from './components/page-header/page-header.component';
//#endregion

const modules = [
  // Abp Modules
  CoreModule,
  ThemeSharedModule,

  // Angular Core Modules
  FormsModule,
  ReactiveFormsModule,

  // Ng Bootstrap
  NgbDropdownModule,
  NgbToastModule,

  // PrimeNg Modules
  AutoCompleteModule,
  CalendarModule,
  CheckboxModule,
  DialogModule,
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
  ProgressBarModule,
  RadioButtonModule,
  SliderModule,
  TableModule,
  TabViewModule,
  TagModule,
  TooltipModule,
  InputSwitchModule,

  // Extension Modules
  NgxValidateCoreModule,
];

const components = [
  PageHeaderComponent,
];

@NgModule({
  declarations: [
    ...components,
  ],
  imports: [
    ...modules,
  ],
  exports: [
    ...modules,
    ...components,
  ],
  providers: []
})
export class SharedModule { }
