import { ListService, LocalizationService, PagedResultDto } from '@abp/ng.core';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MangCTSDto, MangCTSService, TrangThai, trangThaiOptions } from '@app/proxy';
import { Table } from 'primeng/table';

@Component({
    standalone: false,
    selector: 'app-mang-cts',
    templateUrl: './mang-he-thong-cap-cts.component.html',
    styleUrls: ['./mang-he-thong-cap-cts.component.less'],
    providers: [
        ListService,
    ],
})
export class MangHeThongCapCTSComponent implements OnInit {
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    
    mangCTS = { items: [], totalCount: 0 } as PagedResultDto<MangCTSDto>;

    selectedMangCTS = {} as MangCTSDto; // declare selectedToChuc

    form: FormGroup;

    trangThaiOptions = trangThaiOptions;

    isModalOpen = false;

    currentPage = 0;

    pageSize = 10;

    filterString = null;

    constructor(
        public readonly list: ListService,
        private mangCTSService: MangCTSService,
        private fb: FormBuilder,
        private confirmation: ConfirmationService, // inject the ConfirmationService
        private localizationService: LocalizationService
    ) { }

    loadData(event?: { sorting?: string }) {
        const skipCount = this.currentPage * this.pageSize;
        const maxResultCount = this.pageSize;
        const sorting = event?.sorting;
        this.mangCTSService.getList({
            filterInput: this.filterString,
            sorting,
            skipCount,
            maxResultCount,
        }).subscribe((response) => {
            this.mangCTS = response;
        });
        this.dataTable.loading = false;
    }

    ngOnInit() {
        this.loadData();
    }

    getTrangThaiSeverity(value: number | null | undefined): 'success' | 'danger' | 'info' {
        if (value === TrangThai.HoatDong) return 'success';
        if (value === TrangThai.KhongHoatDong) return 'danger';
        return 'info';
    }

    getTrangThai(value: number | null | undefined): string {
        const opt = trangThaiOptions.find(o => o.value === value);
        return opt ? this.localizationService.instant(`::Enum:TrangThai.${opt.key}`) : '';
    }

    createMangCTS() {
        this.selectedMangCTS = {} as MangCTSDto; // reset the selected mangCTS
        this.buildForm();
        this.isModalOpen = true;
    }

    editMangCTS(id: number) {
        this.mangCTSService.get(id).subscribe((mangCTS) => {
            this.selectedMangCTS = mangCTS;
            this.buildForm();
            this.isModalOpen = true;
        });
    }

    deleteMangCTS(id: number) {
        this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure').subscribe((status) => {
            if (status === Confirmation.Status.confirm) {
                this.mangCTSService.delete(id).subscribe(() => this.loadData());
            }
        });
    }

    buildForm() {
        this.form = this.fb.group({
            maMangCTS: [this.selectedMangCTS.maMangCTS || '', Validators.required],
            tenMangCTS: [this.selectedMangCTS.tenMangCTS || '', Validators.required],
            trangThai: [this.selectedMangCTS.trangThai || TrangThai.HoatDong, Validators.required],
            ghiChu: [this.selectedMangCTS.ghiChu || ''],
        });
    }

    save() {
        if (this.form.invalid) {
            return;
        }

        const request = this.selectedMangCTS.id
            ? this.mangCTSService.update(this.selectedMangCTS.id, this.form.value)
            : this.mangCTSService.create(this.form.value);

        request.subscribe(() => {
            this.isModalOpen = false;
            this.form.reset();
            this.loadData();
        });
    }

    onPageChange(event: any) {
        this.currentPage = event.page ?? Math.floor((event.first || 0) / (event.rows || this.pageSize));
		this.pageSize = event.rows || this.pageSize;
        const sortField = event?.sortField;
        const sortOrder = event?.sortOrder;
        const sorting = sortField ? `${sortField} ${sortOrder === 1 ? 'asc' : 'desc'}` : undefined;
        this.loadData({ sorting });
    }

    onSearch(value: string) {
        if (!value) return;
        this.currentPage = 0;
        this.filterString = value;
        this.loadData();
    }

    onCheckMaDuplicate(value: string) {
        if (!value) return;
        this.mangCTSService.checkMaDuplicate(value).subscribe({
            next: (isDuplicate) => {
                const control = this.form.get('maMangCTS');
                if (isDuplicate) {
                    control?.setErrors({ ...control?.errors, duplicate: true });
                } else {
                    if (control?.errors?.duplicate) {
                        const { duplicate, ...otherErrors } = control.errors;
                        control.setErrors(Object.keys(otherErrors).length ? otherErrors : null);
                    }
                }
            },
            error: (err) => {
                console.error('Lỗi khi kiểm tra mã:', err);
            }
        })
    }
}
