import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { FormGroup, FormBuilder, Validators, AbstractControl } from '@angular/forms';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import { ChucVuDto, ChucVuService, trangThaiOptions } from '@app/proxy';
import { MustUniqueChucVuMaDirective } from './chuc-vu.directive';
import { map, of } from 'rxjs';
@Component({
  selector: 'app-chuc-vu',
  templateUrl: './chuc-vu.component.html',
  styleUrls: ['./chuc-vu.component.less'],
  standalone: false,
})
export class ChucVuComponent implements OnInit {
  chucVus = { items: [], totalCount: 0 };

  selectedChucVu = {} as ChucVuDto;
  form!: FormGroup;
  trangThaiOptions = trangThaiOptions;
  isModalOpen = false;
  filterInput = '';
  page = 0;
  pageSize = 5;
  totalCount = 0;
  compareMaValue: string;

  @ViewChild('tenChucVuInput') tenChucVuInput!: ElementRef;

  constructor(
    private chucVuService: ChucVuService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService
  ) {}

  ngOnInit() {
    this.buildForm();
    this.loadData();
  }

  loadData(event?: { sorting?: string }) {
    const skipCount = this.page * this.pageSize;
    const maxResultCount = this.pageSize;
    const sorting = event?.sorting;

    this.chucVuService
      .getList({
        filterInput: this.filterInput,
        sorting,
        skipCount,
        maxResultCount,
      })
      .subscribe(response => {
        this.chucVus = {
          items: response.items ?? [],
          totalCount: response.totalCount,
        };
        this.totalCount = response.totalCount;
      });
  }

  onSearchChange() {
    this.page = 0;
    this.loadData();
  }

  onPageChange(event: any) {
    this.page = event.first / event.rows;
    this.pageSize = event.rows;

    const sortField = event?.sortField;
    const sortOrder = event?.sortOrder;
    const sorting = sortField ? `${sortField} ${sortOrder === 1 ? 'asc' : 'desc'}` : undefined;

    this.loadData({ sorting });
  }

  createChucVu() {
    this.selectedChucVu = {} as ChucVuDto;
    this.buildForm();
    this.isModalOpen = true;

    setTimeout(() => {
      this.tenChucVuInput?.nativeElement?.focus();
    }, 100);
  }

  editChucVu(id: number) {
    this.chucVuService.get(id).subscribe(chucVu => {
      this.selectedChucVu = chucVu;
      this.buildForm();
      this.isModalOpen = true;

      setTimeout(() => {
        this.tenChucVuInput?.nativeElement?.focus();
      }, 100);
    });
  }

  delete(id: number) {
    this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure').subscribe(status => {
      if (status === Confirmation.Status.confirm) {
        this.chucVuService.delete(id).subscribe(() => this.loadData());
      }
    });
  }

  mustUniqueChucVuMaValidator(control: AbstractControl) {
    if (!control.value) {
      return of(null);
    }

    if (this.compareMaValue && this.compareMaValue.trim() === control.value.trim()) {
      return of(null); // không kiểm tra nếu mã không đổi
    }

    return this.chucVuService.isMaChucVuUnique(control.value.trim()).pipe(
      map((isUnique: boolean) => {
        return isUnique ? null : { isValidMustUniqueMa: true };
      })
    );
  }
  buildForm() {
    this.compareMaValue = this.selectedChucVu?.maChucVu?.trim() || '';
    this.form = this.fb.group({
      tenChucVu: [this.selectedChucVu.tenChucVu || '', Validators.required],
      maChucVu: [
        this.selectedChucVu.maChucVu || '',
        Validators.required,
        // 👇 THÊM async validator ở đây:
        [this.mustUniqueChucVuMaValidator.bind(this)],
      ],
      trangThai: [this.selectedChucVu.trangThai ?? 1, Validators.required],
      ghiChu: [this.selectedChucVu.ghiChu || ''],
    });
  }

  save() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
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

  closeModal() {
    this.isModalOpen = false;
    this.form.reset();
  }
}
