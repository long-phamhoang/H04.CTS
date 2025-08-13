import { ListService, PagedResultDto } from '@abp/ng.core';
import { ConfirmationService, Confirmation, ToasterService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgbDateNativeAdapter, NgbDateAdapter } from '@ng-bootstrap/ng-bootstrap';
import { LucLuongDto, LucLuongService, trangThaiOptions, getTrangThaiLabel, TrangThai } from '@app/proxy';

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
export class LucLuongComponent implements OnInit {
	lucLuong = { items: [], totalCount: 0 } as PagedResultDto<LucLuongDto>;

	selectedLucLuong = {} as LucLuongDto;

	form: FormGroup;

	trangThaiOptions = trangThaiOptions;

	isModalOpen = false;

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
	}

	save() {	
		if (this.form.invalid) {
			return;
		}

		const config = { skipHandleError: true };
		const request = this.selectedLucLuong.id
			? this.lucLuongService.update(this.selectedLucLuong.id, this.form.value, config)
			: this.lucLuongService.create(this.form.value, config);
		

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
}


