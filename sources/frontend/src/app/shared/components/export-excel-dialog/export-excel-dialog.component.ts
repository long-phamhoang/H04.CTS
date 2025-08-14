import { Component, Input, Output, EventEmitter, OnInit, OnChanges, SimpleChanges } from '@angular/core';

export interface ExportColumn {
  field: string;
  header?: string;
}

@Component({
  standalone: false,
  selector: 'app-export-excel-dialog',
  templateUrl: './export-excel-dialog.component.html',
  styleUrls: ['./export-excel-dialog.component.less']
})
export class ExportExcelDialogComponent implements OnInit, OnChanges {
  @Input() visible = false;
  @Input() data: any[] = [];
  @Input() columns: ExportColumn[] = [];
  @Input() defaultSelectedFields: string[] = [];
  @Input() defaultFileName = '';
  @Input() defaultSheetName = 'Sheet1';

  @Output() visibleChange = new EventEmitter<boolean>();
  @Output() exported = new EventEmitter<void>();

  fileName = '';
  sheetName = '';
  selectedFields: string[] = [];

  ngOnInit() {
    this.initializeValues();
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['visible'] && changes['visible'].currentValue) {
      this.initializeValues();
    }
  }

  private initializeValues() {
    this.fileName = this.defaultFileName || `export_${new Date().toISOString().slice(0, 10)}.xlsx`;
    this.sheetName = this.defaultSheetName;
    
    if (this.selectedFields.length === 0 && this.columns?.length) {
      this.selectedFields = this.columns.map(c => c.field);
    }
  }

  close() {
    this.visible = false;
    this.visibleChange.emit(false);
  }

  async doExport() {
    const cols = this.columns.filter(c => this.selectedFields.includes(c.field));
    await this.exportToExcel({
      data: this.data,
      columns: cols,
      fileName: this.fileName,
      sheetName: this.sheetName
    });
    this.exported.emit();
    this.close();
  }

  private async exportToExcel(options: {
    data: any[];
    columns: ExportColumn[];
    fileName: string;
    sheetName: string;
  }) {
    // Import xlsx library dynamically
    const XLSX = await import('xlsx');
    
    // Transform data to match column structure
    const exportData = options.data.map(item => {
      const row: any = {};
      options.columns.forEach(col => {
        row[col.header || col.field] = item[col.field];
      });
      return row;
    });

    // Create workbook and worksheet
    const workbook = XLSX.utils.book_new();
    const worksheet = XLSX.utils.json_to_sheet(exportData);

    // Add worksheet to workbook
    XLSX.utils.book_append_sheet(workbook, worksheet, options.sheetName);

    // Save file
    XLSX.writeFile(workbook, options.fileName);
  }
}
