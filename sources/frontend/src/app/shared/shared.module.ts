//#region Abp Modules
import { CoreModule } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
//#endregion
//#region Angular Core Modules
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationService } from 'primeng/api';
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
import { ButtonModule } from 'primeng/button';
import { DropdownModule } from 'primeng/dropdown';
import { TextareaModule } from 'primeng/textarea';
import { SplitButtonModule } from 'primeng/splitbutton';
//#endregion
//#region Extension Modules
import { NgxValidateCoreModule } from '@ngx-validate/core';
//#endregion

//#region Components
import { PageHeaderComponent } from './components/page-header/page-header.component';
import { ExportExcelDialogComponent } from './components/export-excel-dialog/export-excel-dialog.component';
import { ImportExcelDialogComponent } from './components/import-excel-dialog/import-excel-dialog.component';
//#endregion

const modules = [
  // Abp Modules
  CoreModule,
  ThemeSharedModule,

  // Angular Core Modules
  CommonModule,
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
  ConfirmDialogModule,
  FieldsetModule,
  PrimeNGFileUploadModule,
  InputGroupAddonModule,
  InputGroupModule,
  InputNumberModule,
  InputTextModule,
  DropdownModule,
  TextareaModule,
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
  ButtonModule,
  SplitButtonModule,

  // Extension Modules
  NgxValidateCoreModule,
];

const components = [
  PageHeaderComponent,
  ExportExcelDialogComponent,
  ImportExcelDialogComponent,
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
  providers: [
    ConfirmationService
  ]
})
export class SharedModule { }