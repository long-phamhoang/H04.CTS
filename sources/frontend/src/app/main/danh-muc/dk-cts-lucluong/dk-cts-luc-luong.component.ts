import { ListService, PagedResultDto } from '@abp/ng.core';
import { ConfirmationService, Confirmation, ToasterService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgbDateNativeAdapter, NgbDateAdapter } from '@ng-bootstrap/ng-bootstrap';
import { LucLuongDto, LucLuongService, trangThaiOptions, getTrangThaiLabel, TrangThai, DieuKienCapCTSTheoLLDto } from '@app/proxy';
import { DieuKienCapCTSTheoLLService } from '@app/proxy';
import { debounceTime,distinctUntilChanged } from 'rxjs/operators';
import { Subscription } from 'rxjs';

@Component({
    standalone: false,
    selector: 'app-dk-cts-luc-luong',
    templateUrl: './dk-cts-luc-luong.component.html',
    styleUrls: ['./dk-cts-luc-luong.component.less'],
	providers: [
		ListService,
		{ provide: NgbDateAdapter, useClass: NgbDateNativeAdapter }
	],
})
export class DKCtsLucLuongComponent implements OnInit {
    dkCtsLucLuong = { items: [], totalCount: 0 } as PagedResultDto<DieuKienCapCTSTheoLLDto>;

    selected = {} as DieuKienCapCTSTheoLLDto;

	form: FormGroup;

	trangThaiOptions = trangThaiOptions;

    lucLuongOptions: LucLuongDto[] = [];

	isModalOpen = false;

	private maChangeSub: Subscription | null = null;

	// Add these properties to your component class
	searchValue: string = '';
	searchSuggestions: any[] = [];
	showFilter: boolean = false;
	filterName: string = '';
	filterMa: string = '';
	filterStatus: string = '';
	pageSize: number = 10;
	pageIndex: number = 0;
	exportVisible: boolean = false;
	showImport: boolean = false;
	exportData: any[] = [];
	exportColumns: any[] = [];
	importColumns: any[] = [];
	existingMaDieuKienValues: string[] = [];

	constructor(
        public readonly list: ListService,
        private dkService: DieuKienCapCTSTheoLLService,
        private lucLuongService: LucLuongService,
		private fb: FormBuilder,
    private confirmation: ConfirmationService,
    private toast: ToasterService
    ) { }

	ngOnInit() {
        const streamCreator = (query) => this.dkService.getList(query);
        this.list.hookToQuery(streamCreator).subscribe((response) => {
            this.dkCtsLucLuong = response;
        });

        // load luc luong options for select
        this.lucLuongService.getList({ skipCount: 0, maxResultCount: 1000, sorting: 'tenLucLuong' } as any)
            .subscribe((res) => {
                this.lucLuongOptions = res.items || [];
            });
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
        this.selected = {} as DieuKienCapCTSTheoLLDto;
        this.buildForm();
        this.isModalOpen = true;
    }

	edit(id: number) {
        this.dkService.get(id).subscribe((entity) => {
            this.selected = entity;
            this.buildForm();
            this.isModalOpen = true;
        });
	}

    delete(id: number) {
        this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure').subscribe((status) => {
            if (status === Confirmation.Status.confirm) {
                this.dkService.delete(id).subscribe(() => this.list.get());
            }
        });
    }

    buildForm() {
        this.form = this.fb.group({
            maDieuKien: [this.selected.maDieuKien || '', Validators.required],
            tenDieuKien: [this.selected.tenDieuKien || '', Validators.required],
            lucLuongId: [this.selected.lucLuongId || null, Validators.required],
            trangThai: [this.selected.trangThai || null, Validators.required],
            ghiChu: [this.selected.ghiChu || ''],
        });



        
		//check debounce mã 
		const maCtrl = this.form.get('maDieuKien');
		if (this.maChangeSub) {
			this.maChangeSub.unsubscribe();
		}
		this.maChangeSub = maCtrl.valueChanges
			.pipe(debounceTime(300), distinctUntilChanged())
			.subscribe((value) => {
				const ma = (value || '').trim();
				const currentId = this.selected?.id;
				let errors = maCtrl.errors || {};

				if (!ma) {
					delete errors['maDieuKienDuplicated']; 
					maCtrl.setErrors(Object.keys(errors).length ? errors : null);
					return;
				}
				const isDup = (this.dkCtsLucLuong.items || []).some((x: DieuKienCapCTSTheoLLDto) => x.maDieuKien === ma && x.id !== currentId);
				if (isDup) {
					errors['maDieuKienDuplicated'] = true;
				} else {
					delete errors['maDieuKienDuplicated'];
				}
				maCtrl.setErrors(Object.keys(errors).length ? errors : null);
			});
    }

	save() {	
		if (this.form.invalid) {
			return;
		}

        const config = { skipHandleError: true };
        const request = this.selected.id
            ? this.dkService.update(this.selected.id, this.form.value, config)
            : this.dkService.create(this.form.value, config);
		

        request.subscribe({
            next: (e) => {
                this.isModalOpen = false;
                this.form.reset();
                this.list.get();
                this.toast.success('Lưu thành công');
            },
            error: (err) => {
							const message = err?.error?.error?.message || 'Có lỗi xảy ra';
							this.toast.warn(message);
            }
        });
    }

    getLucLuongNameById(id?: number | null): string {
        if (!id) return '';
        const found = this.lucLuongOptions.find(x => x.id === id);
        return found?.tenLucLuong || '';
    }
    // Add these methods to your component class
    onSearch(event: any) {
      // Implement search logic
    }

    onSelect(event: any) {
      // Implement select logic
    }

    clearSearch() {
      this.searchValue = '';
    }

    toggleFilter() {
      this.showFilter = !this.showFilter;
    }

    applyFilter() {
      // Apply filter logic
    }

    clearFilter() {
      this.filterName = '';
      this.filterMa = '';
      this.filterStatus = '';
      this.applyFilter();
    }

    onPageChange(event: any) {
      this.pageIndex = event.page;
      this.pageSize = event.rows;
      // Reload data
    }

    openExportDialog() {
      this.exportVisible = true;
    }

    openImportDialog() {
      this.showImport = true;
    }

    onExported() {
      this.exportVisible = false;
    }

    onImported(data: any) {
      this.showImport = false;
      // Handle imported data
    }
}


