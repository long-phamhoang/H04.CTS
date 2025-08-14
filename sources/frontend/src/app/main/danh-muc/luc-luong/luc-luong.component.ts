import { ListService, PagedResultDto } from '@abp/ng.core';
import { ConfirmationService, Confirmation, ToasterService } from '@abp/ng.theme.shared';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { NgbDateNativeAdapter, NgbDateAdapter } from '@ng-bootstrap/ng-bootstrap';
import { LucLuongDto, LucLuongService, trangThaiOptions, getTrangThaiLabel, TrangThai } from '@app/proxy';
import { ExportColumn } from '@app/shared/components/export-excel-dialog/export-excel-dialog.component';
import { ImportColumn } from 'src/utilities/excel/excelImport';

@Component({
	standalone: false,
	selector: 'app-luc-luong',
	templateUrl: './luc-luong.component.html',
	styleUrls: ['./luc-luong.component.less'],
	providers: [
		ListService,
		{ provide: NgbDateAdapter, useClass: NgbDateNativeAdapter }
	],
})
export class LucLuongComponent implements OnInit, OnDestroy {
	lucLuong = { items: [], totalCount: 0 } as PagedResultDto<LucLuongDto>;

  // pagination state
  pageIndex = 0;
  pageSize = 10;

	selectedLucLuong = {} as LucLuongDto;

	form: FormGroup;

	trangThaiOptions = trangThaiOptions;

	isModalOpen = false;

	private maChangeSub: Subscription | null = null;

	// Autocomplete + filtered view
	allItems: LucLuongDto[] = [];
	displayItems: LucLuongDto[] = [];
	searchValue: any = '';
	searchSuggestions: LucLuongDto[] = [];

	// Export
	exportVisible = false;
	exportColumns: ExportColumn[] = [
		{ field: 'maLucLuong', header: 'Mã' },
		{ field: 'tenLucLuong', header: 'Tên lực lượng' },
		{ field: 'trangThaiText', header: 'Trạng thái' },
		{ field: 'ghiChu', header: 'Ghi chú' },
	];
	exportData: any[] = [];

	// Import
	showImport = false;
	importColumns: ImportColumn[] = [
		{ field: 'maLucLuong', header: 'Mã' },
		{ field: 'tenLucLuong', header: 'Tên lực lượng' },
		{
			field: 'trangThai',
			header: 'Trạng thái',
			parser: (v: any) => {
				const s = (v ?? '').toString().trim().toLowerCase();
				if (s === '1' || s.includes('hoạt')) return TrangThai.HoatDong;
				if (s === '0' || s.includes('không') || s.includes('khong')) return TrangThai.KhongHoatDong;
				const n = Number(s);
				return Number.isFinite(n) ? (n as any) : null;
			},
		},
		{ field: 'ghiChu', header: 'Ghi chú' },
	];

	get existingMaLucLuongValues(): string[] {
		return (this.allItems || []).map(x => (x?.maLucLuong || '').toString());
	}
	
	constructor(
		public readonly list: ListService,
		private lucLuongService: LucLuongService,
		private fb: FormBuilder,
    private confirmation: ConfirmationService,
    private toast: ToasterService
    ) { }

		ngOnInit() {
			const streamCreator = (query) => this.lucLuongService.getList(query);
			this.list.hookToQuery(streamCreator).subscribe((response) => {
				this.lucLuong = response;
				this.allItems = response?.items || [];
				this.displayItems = this.allItems;
				this.list.totalCount = response?.totalCount || 0;
			});
			// initial load with pageSize
			this.list.maxResultCount = this.pageSize;
			this.list.page = 0;
			this.list.get();
		}
	getTrangThai(value: number | null | undefined): string {
		return getTrangThaiLabel(value);
	}

	getTrangThaiSeverity(value: number | null | undefined): 'success' | 'danger' | 'info' {
		if (value === TrangThai.HoatDong) return 'success';
		if (value === TrangThai.KhongHoatDong) return 'danger';
		return 'info';
	}

	create() {
		this.selectedLucLuong = {} as LucLuongDto;
		this.buildForm();
		this.isModalOpen = true;
	}

	edit(id: number) {
		this.lucLuongService.get(id).subscribe((entity) => {
			this.selectedLucLuong = entity;
			this.buildForm();
			this.isModalOpen = true;
		});
	}

	delete(id: number) {
		this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure').subscribe((status) => {
			if (status === Confirmation.Status.confirm) {
				this.lucLuongService.delete(id).subscribe(() => this.list.get());
			}
		});
	}

	buildForm() {
		this.form = this.fb.group({
			tenLucLuong: [this.selectedLucLuong.tenLucLuong || '', Validators.required],
			maLucLuong: [this.selectedLucLuong.maLucLuong || '', Validators.required],
			trangThai: [this.selectedLucLuong.trangThai || null, Validators.required],
			ghiChu: [this.selectedLucLuong.ghiChu || ''],
		});

		//check debounce mã 
		const maCtrl = this.form.get('maLucLuong');
		if (this.maChangeSub) {
			this.maChangeSub.unsubscribe();
		}
		this.maChangeSub = maCtrl.valueChanges
			.pipe(debounceTime(300), distinctUntilChanged())
			.subscribe((value) => {
				const ma = (value || '').trim();
				const currentId = this.selectedLucLuong?.id;
				let errors = maCtrl.errors || {};

				if (!ma) {
					delete errors['maLucLuongDuplicated'];
					maCtrl.setErrors(Object.keys(errors).length ? errors : null);
					return;
				}
				const isDup = (this.lucLuong.items || []).some((x: LucLuongDto) => x.maLucLuong === ma && x.id !== currentId);
				if (isDup) {
					errors['maLucLuongDuplicated'] = true;
				} else {
					delete errors['maLucLuongDuplicated'];
				}
				maCtrl.setErrors(Object.keys(errors).length ? errors : null);
			});
	}

	ngOnDestroy(): void {
		if (this.maChangeSub) {
			this.maChangeSub.unsubscribe();
		}
	}

	// AutoComplete handlers
onSearch(event: { query: string }) {
	const q = (event?.query || '').trim();
	this.searchSuggestions = [];
	this.list.filter = q;
	this.list.page = 0;
	// keep current pageSize
	this.list.maxResultCount = this.pageSize;
	this.list.get();
}

onSelect(_: any) {
}

clearSearch() {
	this.searchValue = '';
	this.searchSuggestions = [];
	this.list.filter = '';
	this.list.page = 0;
	this.list.maxResultCount = this.pageSize;
	this.list.get();
}

		onPageChange(event: any) {
			this.pageIndex = event.page ?? Math.floor((event.first || 0) / (event.rows || this.pageSize));
			this.pageSize = event.rows || this.pageSize;
	
			this.list.maxResultCount = this.pageSize;
			this.list.page = this.pageIndex;
			this.list.getWithoutPageReset();
		}

	// restore save() method
	save() {
		if (!this.form || this.form.invalid) return;

		const config = { skipHandleError: true };
		const request = this.selectedLucLuong.id
			? this.lucLuongService.update(this.selectedLucLuong.id, this.form.value, config)
			: this.lucLuongService.create(this.form.value, config);

		request.subscribe({
			next: () => {
				this.isModalOpen = false;
				this.form.reset();
				this.list.get();
				this.toast.success('Lưu thành công');
			},
			error: (err) => {
				const message = err?.error?.error?.message || 'Có lỗi xảy ra';
				this.toast.warn(message);
			},
		});
	}

	// Export
	openExportDialog() {
		const mapTrangThai = (v: number | null | undefined) => this.getTrangThai(v);
		this.exportData = (this.displayItems || []).map(x => ({
			...x,
			trangThaiText: mapTrangThai(x?.trangThai),
		}));
		this.exportVisible = true;
	}

	onExported() {
		this.toast.success('Xuất Excel thành công');
	}

	// Import
	openImportDialog() {
		this.showImport = true;
	}

	onImported(rows: any[]) {
		if (!rows?.length) return;
		let ok = 0, fail = 0;

		const existingCodes = new Set<string>((this.allItems || []).map(x => (x?.maLucLuong || '').toString().trim().toLowerCase()));
		const seenInImport = new Set<string>();

		const doNext = (i: number) => {
			if (i >= rows.length) {
				this.list.get();
				this.toast.success(`Import xong. Thành công: ${ok}, Lỗi: ${fail}`);
				return;
			}
			const r = rows[i] || {};
			const dto = {
				maLucLuong: (r.maLucLuong || '').toString().trim(),
				tenLucLuong: (r.tenLucLuong || '').toString().trim(),
				trangThai: r.trangThai ?? null,
				ghiChu: r.ghiChu || '',
			};

			const code = dto.maLucLuong.toLowerCase();
			if (!code || existingCodes.has(code) || seenInImport.has(code)) {
				fail++;
				doNext(i + 1);
				return;
			}

			seenInImport.add(code);

			this.lucLuongService.create(dto as any, { skipHandleError: true }).subscribe({
				next: () => { ok++; existingCodes.add(code); doNext(i + 1); },
				error: () => { fail++; doNext(i + 1); },
			});
		};
		doNext(0);
	}
}