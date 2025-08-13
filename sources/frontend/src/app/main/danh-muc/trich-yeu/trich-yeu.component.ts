import { ListService, LocalizationService, PagedResultDto } from '@abp/ng.core';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { TrangThai, TrichYeuDto, TrichYeuService, trangThaiOptions } from '@app/proxy';
import { Table } from 'primeng/table';

@Component({
    standalone: false,
    selector: 'app-trich-yeu',
    templateUrl: './trich-yeu.component.html',
    styleUrls: ['./trich-yeu.component.less'],
    providers: [
        ListService,
    ],
})
export class TrichYeuComponent implements OnInit {
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    
    trinhYeu = { items: [], totalCount: 0 } as PagedResultDto<TrichYeuDto>;

    selectedTrichYeu = {} as TrichYeuDto; // declare selectedToChuc

    form: FormGroup;

    trangThaiOptions = trangThaiOptions;

    isModalOpen = false;

    currentPage = 0;

    pageSize = 10;

    filterString = null;

    constructor(
        public readonly list: ListService,
        private trichYeuService: TrichYeuService,
        private fb: FormBuilder,
        private confirmation: ConfirmationService, // inject the ConfirmationService
        private localizationService: LocalizationService
    ) { }

    loadData(event?: { sorting?: string }) {
        const skipCount = this.currentPage * this.pageSize;
        const maxResultCount = this.pageSize;
        const sorting = event?.sorting;
        this.trichYeuService.getList({
            filterInput: this.filterString,
            sorting,
            skipCount,
            maxResultCount,
        }).subscribe((response) => {
            this.trinhYeu = response;
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

    createTrichYeu() {
        this.selectedTrichYeu = {} as TrichYeuDto;
        this.buildForm();
        this.isModalOpen = true;
    }

    editTrichYeu(id: number) {
        this.trichYeuService.get(id).subscribe((trichYeu) => {
            this.selectedTrichYeu = trichYeu;
            this.buildForm();
            this.isModalOpen = true;
        });
    }

    deleteTrichYeu(id: number) {
        this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure').subscribe((status) => {
            if (status === Confirmation.Status.confirm) {
                this.trichYeuService.delete(id).subscribe(() => this.loadData());
            }
        });
    }

    buildForm() {
        this.form = this.fb.group({
            maTrichYeu: [this.selectedTrichYeu.maTrichYeu || '', Validators.required],
            tenTrichYeu: [this.selectedTrichYeu.tenTrichYeu || '', Validators.required],
            trangThai: [this.selectedTrichYeu.trangThai || TrangThai.HoatDong, Validators.required],
            moTa: [this.selectedTrichYeu.moTa || ''],
            ghiChu: [this.selectedTrichYeu.ghiChu || ''],
        });
    }

    save() {
        if (this.form.invalid) {
            return;
        }

        const request = this.selectedTrichYeu.id
            ? this.trichYeuService.update(this.selectedTrichYeu.id, this.form.value)
            : this.trichYeuService.create(this.form.value);

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
        this.trichYeuService.checkMaDuplicate(value).subscribe({
            next: (isDuplicate) => {
                const control = this.form.get('maTrichYeu');
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
