// sources/frontend/src/app/shared/components/import-excel-dialog/import-excel-dialog.component.ts
import { Component, EventEmitter, Input, OnChanges, Output, SimpleChanges, ViewChild, ElementRef } from '@angular/core';
import { downloadImportTemplate, parseExcelFile, ImportColumn } from '../../../../utilities/excel/excelImport';

@Component({
	standalone: false,
	selector: 'app-import-excel-dialog',
	templateUrl: './import-excel-dialog.component.html',
	styleUrls: ['./import-excel-dialog.component.less'],
})
export class ImportExcelDialogComponent implements OnChanges {
	@Input() visible = false;
	@Input() columns: ImportColumn[] = [];
	@Input() templateFileName?: string;
	@Input() templateSheetName?: string;
	@Input() requiredFields?: string[];
	@Input() uniqueByField?: string;
	@Input() existingValues: Array<string | number> = [];

	@Output() visibleChange = new EventEmitter<boolean>();
	@Output() imported = new EventEmitter<any[]>();

	@ViewChild('fileInput') fileInput?: ElementRef<HTMLInputElement>;
	isSubmitting = false;
	rows: any[] = [];
	sheetName = 'Sheet1';
	rowIssues: string[][] = [];

	get requiredEffective(): string[] {
		return this.requiredFields && this.requiredFields.length
			? this.requiredFields
			: this.columns.map(c => c.field);
	}

	get invalidCount(): number {
		return this.rowIssues.filter(arr => arr.length > 0).length;
	}

	get validCount(): number {
		return this.rows.length - this.invalidCount;
	}

	ngOnChanges(changes: SimpleChanges): void {
		if (changes['visible'] && changes['visible'].currentValue) {
			this.columns = this.columns || [];
			this.sheetName = this.templateSheetName || 'Sheet1';
			this.rows = [];
			this.rowIssues = [];
			this.clearFileInput();
		}
	}

	private headerOf(field: string): string {
		const col = this.columns.find(c => c.field === field);
		return col?.header || field;
	}

	private normalize(value: any) {
		return (value ?? '').toString().trim().toLowerCase();
	}

	private validateRows(): void {
		const req = this.requiredEffective;
		const uniqField = this.uniqueByField;

		const existingSet = new Set((this.existingValues || []).map(v => this.normalize(v)));
		const counts: Record<string, number> = {};
		if (uniqField) {
			for (const r of this.rows) {
				const key = this.normalize(r[uniqField]);
				if (key) counts[key] = (counts[key] || 0) + 1;
			}
		}

		this.rowIssues = this.rows.map(r => {
			const issues: string[] = [];
			for (const f of req) {
				const v = r[f];
				const text = (v ?? '').toString().trim();
				if (!text) {
					issues.push(`${this.headerOf(f)} là bắt buộc`);
				}
			}
			if (uniqField) {
				const key = this.normalize(r[uniqField]);
				if (key) {
					if (counts[key] > 1) issues.push(`${this.headerOf(uniqField)} bị trùng trong file`);
					if (existingSet.has(key)) issues.push(`${this.headerOf(uniqField)} đã tồn tại`);
				}
			}
			return issues;
		});
	}

	async downloadTemplate(): Promise<void> {
		await downloadImportTemplate({
			columns: this.columns,
			fileName: this.templateFileName || 'import_template.xlsx',
			sheetName: this.sheetName,
		});
	}

	async onFileChange(e: Event): Promise<void> {
		const input = e.target as HTMLInputElement;
		const file = input.files && input.files[0];
		if (!file) return;

		this.rows = await parseExcelFile({
			file,
			columns: this.columns,
			sheetName: this.sheetName,
		});
		this.validateRows();
	}

	close(): void {
		this.visible = false;
		this.visibleChange.emit(false);
		this.rows = [];
		this.rowIssues = [];
		this.clearFileInput();
	}

	private clearFileInput() {
		const el = this.fileInput?.nativeElement;
		if (el) {
			el.value = '';
			// Some browsers need a type toggle to fully reset
			el.type = 'text';
			el.type = 'file';
		}
	}

	async confirmImport(): Promise<void> {
		this.isSubmitting = true;
		try {
			this.imported.emit(this.rows);
			this.close();
		} finally {
			this.isSubmitting = false;
		}
	}
}