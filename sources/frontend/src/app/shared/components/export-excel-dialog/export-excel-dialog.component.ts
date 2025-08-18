import { Component, Input, Output, EventEmitter, OnInit, OnChanges, SimpleChanges } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';

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
  // API options for fetching data before export
  @Input() apiUrl?: string;
  @Input() requestParams?: Record<string, any> | null = null;
  @Input() responsePath?: string | null = null;
  @Input() mapResponse?: (resp: any) => any[];
  @Input() fetcher?: (params?: any) => Promise<any> | any;

  @Output() visibleChange = new EventEmitter<boolean>();
  @Output() exported = new EventEmitter<void>();

  
  fileName = '';
  sheetName = '';
  selectedFields: string[] = [];

  // trạng thái tải dữ liệu
  loading = false;
  error?: string;

  constructor(private http: HttpClient) {}
  
  // Tạo options cho multiselect
  get columnOptions() {
    return this.columns.map(col => ({
      label: col.header || col.field,
      value: col.field
    }));
  }

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
    try {
      this.loading = true;
      const cols = this.columns.filter(c => this.selectedFields.includes(c.field));
      const shouldFetch = !!this.fetcher || this.hasApiUrl();
      const data: any[] = shouldFetch ? ((await this.fetchData()) ?? []) : (this.data ?? []);
      await this.exportToExcel({
        data,
        columns: cols,
        fileName: this.fileName,
        sheetName: this.sheetName
      });
      this.exported.emit();
      this.close();
    } catch (err) {
      console.error('Export failed', err);
      this.error = 'Không thể xuất dữ liệu. Vui lòng thử lại.';
    } finally {
      this.loading = false;
    }
  }

  private hasApiUrl(): boolean {
    const url = (this.apiUrl || '').toString().trim();
    return url.length > 0;
  }

  private buildHttpParams(obj?: Record<string, any> | null): HttpParams | undefined {
    if (!obj) return undefined;
    let params = new HttpParams();
    Object.entries(obj).forEach(([key, value]) => {
      if (value === undefined || value === null) {
        return;
      }
      if (Array.isArray(value)) {
        value.forEach(v => { params = params.append(key, String(v)); });
      } else {
        params = params.set(key, String(value));
      }
    });
    return params;
  }

  private getByPath(obj: any, path: string): any {
    return path.split('.').reduce((acc: any, key: string) => acc?.[key], obj);
  }

  private async fetchData(): Promise<any[]> {
    // Use custom fetcher first if provided
    if (this.fetcher) {
      const resp = await this.fetcher(this.requestParams);
      if (this.mapResponse) return this.mapResponse(resp) ?? [];
      if (this.responsePath) {
        const resolved = this.getByPath(resp, this.responsePath);
        return Array.isArray(resolved) ? resolved : [];
      }
      if (Array.isArray(resp)) return resp;
      if (resp && Array.isArray((resp as any).items)) return (resp as any).items;
      return [];
    }

    if (!this.hasApiUrl()) {
      return this.data ?? [];
    }
    try {
      const params = this.buildHttpParams(this.requestParams);
      const resp = await firstValueFrom(this.http.get(this.apiUrl!, { params }));
      if (this.mapResponse) {
        return this.mapResponse(resp) ?? [];
      }
      if (this.responsePath) {
        const resolved = this.getByPath(resp, this.responsePath);
        return Array.isArray(resolved) ? resolved : [];
      }
      if (Array.isArray(resp)) {
        return resp;
      }
      if (resp && Array.isArray((resp as any).items)) {
        return (resp as any).items;
      }
      return [];
    } catch (e) {
      console.error('Fetch data for export failed', e);
      return [];
    }
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
