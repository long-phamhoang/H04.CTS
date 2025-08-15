import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import { ChucVuDto, ChucVuService, trangThaiOptions } from '@app/proxy';

@Component({
  selector: 'app-chuc-vu',
  templateUrl: './chuc-vu.component.html',
  styleUrls: ['./chuc-vu.component.less'],
  standalone: false,
})
export class ChucVuComponent implements OnInit {
  chucVus = { items: [], totalCount: 0 };

  selectedChucVu = {} as ChucVuDto;
  form: FormGroup;
  trangThaiOptions = trangThaiOptions;
  isModalOpen = false;
  filterInput = '';
  page = 0;
  pageSize = 5;
  totalCount = 0;

  constructor(
    private chucVuService: ChucVuService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService
  ) {}

  ngOnInit() {
    this.loadData();
  }

  loadData() {
    const skipCount = this.page * this.pageSize;
    const maxResultCount = this.pageSize;

    this.chucVuService.getList({ filterInput: this.filterInput, skipCount, maxResultCount }).subscribe(response => {
      this.chucVus = {
        items: response.items ?? [],
        totalCount: response.totalCount
      };
      this.totalCount = response.totalCount;
    });
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

  createChucVu() {
    this.selectedChucVu = {} as ChucVuDto;
    this.buildForm();
    this.isModalOpen = true;
  }

  editChucVu(id: number) {
    this.chucVuService.get(id).subscribe(chucVu => {
      this.selectedChucVu = chucVu;
      this.buildForm();
      this.isModalOpen = true;
    });
  }

  delete(id: number) {
    this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure').subscribe(status => {
      if (status === Confirmation.Status.confirm) {
        this.chucVuService.delete(id).subscribe(() => this.loadData());
      }
    });
  }

  buildForm() {
    this.form = this.fb.group({
      tenChucVu: [this.selectedChucVu.tenChucVu || '', Validators.required],
      maChucVu: [this.selectedChucVu.maChucVu || '', Validators.required],
      trangThai: [this.selectedChucVu.trangThai ?? 1, Validators.required],
      ghiChu: [this.selectedChucVu.ghiChu || ''],
    });
  }

  save() {
    if (this.form.invalid) {
      return;
    }

    const request = this.selectedChucVu.id
      ? this.chucVuService.update(this.selectedChucVu.id, this.form.value)
      : this.chucVuService.create(this.form.value);

    request.subscribe(() => {
      this.isModalOpen = false;
      this.form.reset();
      this.loadData();
    });
  }
}
