import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import { ChucVuService, ThueBaoCaNhanDto, ToChucService } from '@app/proxy';
import { ThueBaoCaNhanService } from '@app/proxy/services/thue-bao-ca-nhan.service';
import { PROVINCES_MOCK } from '@app/proxy/mock-data-location';
import { Table } from 'primeng/table';

@Component({
  selector: 'app-chuc-vu',
  templateUrl: './thue-bao-ca-nhan.component.html',
  styleUrls: ['./thue-bao-ca-nhan.component.less'],
  standalone: false,
})
export class ThueBaoCaNhanComponent implements OnInit {
  @ViewChild('dataTable', { static: true }) dataTable: Table;
  @ViewChild('hoTenInput') hoTenInput: any;
  thueBaoCaNhans = { items: [], totalCount: 0 };

  selectedThueBaoCaNhan = {} as ThueBaoCaNhanDto;
  form: FormGroup;

  isModalOpen = false;
  filterInput = '';
  chucVuOptions: { id: number; name: string }[] =[];
  toChucOptions: { id: number; name: string }[] =[];
  tinhThanhPhoOptions: { id: number; name: string }[] = [];
  phuongXaOptions: { id: number; name: string }[] = [];
  page = 0;
  pageSize = 5;
  totalCount = 0;

  constructor(
    private thueBaoCaNhanService: ThueBaoCaNhanService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService,
    private _chucVuService: ChucVuService,
    private _toChucService: ToChucService
  ) {
    // Khởi tạo form rỗng tránh lỗi undefined
    this.form = this.fb.group({
      hoTen: [''],
      ngaySinh: [''],
      soDinhDanhCaNhan: [''],
      noiCap: [''],
      ngayCap: [''],
      toChucId: [''],
      chucVuId: [''],
      diaChiThuDienTuCongVu: [''],
      tinhThanhPho: [null],
      phuongXa: [null],
    });
  }

  ngOnInit() {
    this.loadData();
    this.loadDropdowns();
    this.loadTinhThanhPho();
  }

  loadData(event?: { sorting?: string }) {
    const skipCount = this.page * this.pageSize;
    const maxResultCount = this.pageSize;
    const sorting = event?.sorting;

    this.thueBaoCaNhanService
      .getList({
        filterInput: this.filterInput,
        sorting,
        skipCount,
        maxResultCount,
      })
      .subscribe(response => {
        this.thueBaoCaNhans = {
          items: response.items ?? [],
          totalCount: response.totalCount,
        };
        this.totalCount = response.totalCount;
      });

    const selectedTinh = this.form?.value?.tinhThanhPho;
    if (selectedTinh) {
      this.onTinhThanhPhoChange(selectedTinh);
    }

    this.dataTable.loading = false;
  }

  loadDropdowns() {
    this._chucVuService.getChucVuDropdown().subscribe(data => {
      this.chucVuOptions = data.map(x => ({ id: x.id, name: x.tenChucVu }));
    });

    this._toChucService.getToChucDropdown().subscribe(data => {
      this.toChucOptions = data.map(x => ({ id: x.id, name: x.tenToChuc }));
    });
  }

  loadTinhThanhPho() {
    this.tinhThanhPhoOptions = PROVINCES_MOCK.map(p => ({
      id: p.id,
      name: p.name,
    }));
  }

  onTinhThanhPhoChange(tinhId: number): void {
    const selected = PROVINCES_MOCK.find(p => p.id === +tinhId);
    this.phuongXaOptions = selected?.communes ?? [];
  }

  onSearchChange() {
    this.page = 0; // Reset to first page on search
    this.loadData();
  }

  onPageChange(event: any) {
    this.page = event.first / event.rows;
    this.pageSize = event.rows;
    const sortField = event?.sortField;
    const sortOrder = event?.sortOrder; // 1 asc, -1 desc
    const sorting = sortField ? `${sortField} ${sortOrder === 1 ? 'asc' : 'desc'}` : undefined;
    this.loadData({ sorting });
  }

  totalPages(): number {
    if (!this.pageSize || this.pageSize <= 0) return 1;
    return Math.ceil(this.totalCount / this.pageSize) || 1;
  }

  createthueBaoCaNhan() {
    this.selectedThueBaoCaNhan = {} as ThueBaoCaNhanDto;
    this.buildForm();
    this.isModalOpen = true;
    setTimeout(() => {
      this.hoTenInput?.nativeElement?.focus();
    }, 100);
  }

  editThueBaoCaNhan(id: number) {
    this.thueBaoCaNhanService.get(id).subscribe(thueBaoCaNhan => {
      this.selectedThueBaoCaNhan = thueBaoCaNhan;
      this.buildForm();

      // Gọi sau khi form đã được build xong
      const selectedTinh = this.selectedThueBaoCaNhan.tinhThanhPho;
      if (selectedTinh) {
        this.onTinhThanhPhoChange(selectedTinh);
      }

      this.isModalOpen = true;
    });
  }

  delete(id: number) {
    this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure').subscribe(status => {
      if (status === Confirmation.Status.confirm) {
        this.thueBaoCaNhanService.delete(id).subscribe(() => this.loadData());
      }
    });
  }

  buildForm() {
    const formatDateToInput = (date: any) => {
      if (!date) return null;
      const d = new Date(date);
      const year = d.getFullYear();
      const month = String(d.getMonth() + 1).padStart(2, '0');
      const day = String(d.getDate()).padStart(2, '0');
      return `${year}-${month}-${day}`;
    };

    this.form = this.fb.group({
      hoTen: [this.selectedThueBaoCaNhan.hoTen || '', Validators.required],
      ngaySinh: [formatDateToInput(this.selectedThueBaoCaNhan.ngaySinh), Validators.required],
      soDinhDanhCaNhan: [this.selectedThueBaoCaNhan.soDinhDanhCaNhan || '', Validators.required],
      noiCap: [this.selectedThueBaoCaNhan.noiCap || '', Validators.required],
      ngayCap: [formatDateToInput(this.selectedThueBaoCaNhan.ngayCap), Validators.required],
      toChucId: [this.selectedThueBaoCaNhan.toChucId || '', Validators.required],
      chucVuId: [this.selectedThueBaoCaNhan.chucVuId || '', Validators.required],
      diaChiThuDienTuCongVu: [
        this.selectedThueBaoCaNhan.diaChiThuDienTuCongVu || '',
        Validators.required,
      ],
      tinhThanhPho: [this.selectedThueBaoCaNhan.tinhThanhPho || null],
      phuongXa: [this.selectedThueBaoCaNhan.phuongXa || null],
    });
  }

  save() {
    if (this.form.invalid) {
      return;
    }

    const formValue = { ...this.form.value };

    // Chuyển thành Date nếu chưa phải Date
    if (formValue.ngaySinh && !(formValue.ngaySinh instanceof Date)) {
      formValue.ngaySinh = new Date(formValue.ngaySinh);
    }
    if (formValue.ngayCap && !(formValue.ngayCap instanceof Date)) {
      formValue.ngayCap = new Date(formValue.ngayCap);
    }

    // Sau đó mới gọi toISOString()
    if (formValue.ngaySinh) {
      formValue.ngaySinh = formValue.ngaySinh.toISOString().split('T')[0];
    }
    if (formValue.ngayCap) {
      formValue.ngayCap = formValue.ngayCap.toISOString().split('T')[0];
    }

    const request = this.selectedThueBaoCaNhan.id
      ? this.thueBaoCaNhanService.update(this.selectedThueBaoCaNhan.id, formValue)
      : this.thueBaoCaNhanService.create(formValue);

    request.subscribe(() => {
      this.isModalOpen = false;
      this.form.reset();
      this.loadData();
    });
  }
}
