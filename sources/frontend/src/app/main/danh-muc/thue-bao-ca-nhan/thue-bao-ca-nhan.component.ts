import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import { ChucVuService, ThueBaoCaNhanDto, ToChucService } from '@app/proxy';
import { ThueBaoCaNhanService } from '@app/proxy/services/thue-bao-ca-nhan.service';
import { PROVINCES_MOCK } from '@app/proxy/mock-data-location';
@Component({
  selector: 'app-chuc-vu',
  templateUrl: './thue-bao-ca-nhan.component.html',
  styleUrls: ['./thue-bao-ca-nhan.component.less'],
  standalone: false,
})
export class ThueBaoCaNhanComponent implements OnInit {
  thueBaoCaNhans = { items: [], totalCount: 0 };

  selectedThueBaoCaNhan = {} as ThueBaoCaNhanDto;
  form: FormGroup;
  isModalOpen = false;
  filterInput = '';
  chucVuOptions = [];
  toChucOptions = [];
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
  ) {}

  ngOnInit() {
    this.loadData();
    this.loadDropdowns();
    this.loadTinhThanhPho();
  }

  loadData() {
    const skipCount = this.page * this.pageSize;
    const maxResultCount = this.pageSize;

    this.thueBaoCaNhanService
      .getList({ filterInput: this.filterInput, skipCount, maxResultCount })
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
  }
  loadDropdowns() {
    this._chucVuService.getChucVuDropdown().subscribe(data => {
      this.chucVuOptions = data.map(x => ({ id: x.id, name: x.tenChucVu }));
    });

    this._toChucService.getToChucDropdown().subscribe(data => {
      this.toChucOptions = data.map(x => ({ id: x.id, name: x.tenToChuc }));
    });

    // Nếu có API thì load tỉnh/thành và phường/xã ở đây
    this.tinhThanhPhoOptions = []; // optional
    this.phuongXaOptions = []; // optional
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
    this.loadData();
  }

  totalPages(): number {
    if (!this.pageSize || this.pageSize <= 0) return 1;
    return Math.ceil(this.totalCount / this.pageSize) || 1;
  }

  createthueBaoCaNhan() {
    this.selectedThueBaoCaNhan = {} as ThueBaoCaNhanDto;
    this.buildForm();
    this.isModalOpen = true;
  }

  editthueBaoCaNhan(id: number) {
    this.thueBaoCaNhanService.get(id).subscribe(thueBaoCaNhan => {
      this.selectedThueBaoCaNhan = thueBaoCaNhan;
      this.buildForm();
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
    this.form = this.fb.group({
      hoTen: [this.selectedThueBaoCaNhan.hoTen || '', Validators.required],
      ngaySinh: [this.selectedThueBaoCaNhan.ngaySinh || '', Validators.required],
      soDinhDanhCaNhan: [this.selectedThueBaoCaNhan.soDinhDanhCaNhan || '', Validators.required],
      noiCap: [this.selectedThueBaoCaNhan.noiCap || '', Validators.required],
      ngayCap: [this.selectedThueBaoCaNhan.ngayCap || '', Validators.required],
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

    const request = this.selectedThueBaoCaNhan.id
      ? this.thueBaoCaNhanService.update(this.selectedThueBaoCaNhan.id, this.form.value)
      : this.thueBaoCaNhanService.create(this.form.value);

    request.subscribe(() => {
      this.isModalOpen = false;
      this.form.reset();
      this.loadData();
    });
  }
}
