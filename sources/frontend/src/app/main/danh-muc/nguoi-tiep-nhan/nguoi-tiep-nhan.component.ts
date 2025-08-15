import { ListService, PagedResultDto } from '@abp/ng.core';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgbDateNativeAdapter, NgbDateAdapter } from '@ng-bootstrap/ng-bootstrap';
import { NguoiTiepNhanDto, NguoiTiepNhanService, trangThaiOptions, NoiCapCCCDService, NoiCapCCCDDto, ToChucService, ToChucDto } from '@app/proxy';

@Component({
  standalone: false,
  selector: 'app-nguoi-tiep-nhan',
  templateUrl: './nguoi-tiep-nhan.component.html',
  styleUrls: ['./nguoi-tiep-nhan.component.less'],
  providers: [
    ListService,
    { provide: NgbDateAdapter, useClass: NgbDateNativeAdapter }
  ],
})
export class NguoiTiepNhanComponent implements OnInit {
  nguoiTiepNhan = { items: [], totalCount: 0 } as PagedResultDto<NguoiTiepNhanDto>;

  selectedNguoiTiepNhan = {} as NguoiTiepNhanDto; // declare selectedNguoiTiepNhan

  form: FormGroup;

  trangThaiOptions = trangThaiOptions;

  isModalOpen = false;

  noiCapCCCDs: NoiCapCCCDDto[] = [];
  organizations: ToChucDto[] = [];

	pageSize = 10;

  keyword: string = '';

  constructor(
    public readonly list: ListService,
    private nguoiTiepNhanService: NguoiTiepNhanService,
    private fb: FormBuilder,
    private noiCapCCCDService: NoiCapCCCDService,
    private toChucService: ToChucService,
    private confirmation: ConfirmationService // inject the ConfirmationService
  ) { }

  ngOnInit() {
    // initial load
    // this.reload();

    // Load NoiCapCCCD options for select
    this.noiCapCCCDService
      .getList({ skipCount: 0, maxResultCount: 1000, sorting: 'name' })
      .subscribe(res => {
        this.noiCapCCCDs = res.items ?? [];
      });

    // Load Organizations options for select
    this.toChucService
      .getList({ skipCount: 0, maxResultCount: 1000, sorting: 'tenToChuc' })
      .subscribe(res => {
        this.organizations = res.items ?? [];
      });
  }

	loadNguoiTiepNhan(event: any) {
		const rows = event?.rows || this.pageSize;
		const first = event?.first || 0;
		const sortField = event?.sortField;
		const sortOrder = event?.sortOrder; // 1 asc, -1 desc
		const sorting = sortField ? `${sortField} ${sortOrder === 1 ? 'asc' : 'desc'}` : undefined;
		this.reload({ first, rows, sorting });
	}

	reload(event?: { first?: number; rows?: number; sorting?: string }) {
		const first = event?.first ?? 0;
		const rows = event?.rows ?? this.pageSize;
		const sorting = event?.sorting;
		this.nguoiTiepNhanService
			.getList({ keyword: this.keyword, skipCount: first, maxResultCount: rows, sorting })
			.subscribe((response) => (this.nguoiTiepNhan = response));
	}

  onSearch() {
    this.reload({ first: 0, rows: this.pageSize });
  }

  createNguoiTiepNhan() {
    this.selectedNguoiTiepNhan = {} as NguoiTiepNhanDto; // reset the selected NguoiTiepNhan
    this.buildForm();
    this.isModalOpen = true;
  }

  editNguoiTiepNhan(id: number) {
    this.nguoiTiepNhanService.get(id).subscribe((nguoiTiepNhan) => {
      this.selectedNguoiTiepNhan = nguoiTiepNhan;
      this.buildForm();
      this.isModalOpen = true;
    });
  }

  delete(id: number) {
    this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure').subscribe((status) => {
      if (status === Confirmation.Status.confirm) {
        this.nguoiTiepNhanService.delete(id).subscribe(() => this.reload());
      }
    });
  }

  buildForm() {
    this.form = this.fb.group({
      organizationId: [this.selectedNguoiTiepNhan.organizationId || null],
      fullName: [this.selectedNguoiTiepNhan.fullName || '', Validators.required],
      cccd: [this.selectedNguoiTiepNhan.cccd || null, Validators.required],
      dateOfIssue: [this.toDateStruct(this.selectedNguoiTiepNhan.dateOfIssue) || null, Validators.required],
      noiCapCCCDId: [this.selectedNguoiTiepNhan.noiCapCCCDId || null],
      position: [this.selectedNguoiTiepNhan.position || null, Validators.required],
      phone: [this.selectedNguoiTiepNhan.phone || null, Validators.required],
      email: [this.selectedNguoiTiepNhan.email || null, Validators.required],
      submissionAddress: [this.selectedNguoiTiepNhan.submissionAddress || null, Validators.required],
      province: [this.selectedNguoiTiepNhan.province || null, Validators.required],
      ward: [this.selectedNguoiTiepNhan.ward || null, Validators.required],
      isDefault: [this.selectedNguoiTiepNhan.isDefault || null, Validators.required],
    });
  }

  // change the save method
  save() {
    if (this.form.invalid) {
      this.logFormErrors();
      return;
    }

    const raw = this.form.value as any;

    const toNullableNumber = (v: any) => {
      if (v === null || v === undefined || v === '') return null;
      const n = typeof v === 'number' ? v : Number(v);
      return Number.isNaN(n) ? null : n;
    };

    const dto: any = {
      ...raw,
      organizationId: toNullableNumber(raw.organizationId),
      noiCapCCCDId: toNullableNumber(raw.noiCapCCCDId),
      dateOfIssue: this.toIsoDate(raw.dateOfIssue),
      isDefault: typeof raw.isDefault === 'string' ? raw.isDefault === 'true' : !!raw.isDefault,
    };

    const request = this.selectedNguoiTiepNhan.id
      ? this.nguoiTiepNhanService.update(this.selectedNguoiTiepNhan.id, dto)
      : this.nguoiTiepNhanService.create(dto);

    request.subscribe(() => {
      this.isModalOpen = false;
      this.form.reset();
      this.reload();
    });
  }

  private toDateStruct(input?: string | Date | null): any {
    if (!input) return null;
    const d = typeof input === 'string' ? new Date(input) : input;
    if (isNaN(d.getTime())) return null;
    return { year: d.getFullYear(), month: d.getMonth() + 1, day: d.getDate() };
  }

  private toIsoDate(struct: any): string | null {
    if (!struct) return null;
    if (struct instanceof Date) return struct.toISOString();
    const { year, month, day } = struct || {};
    if (!year || !month || !day) return null;
    const dt = new Date(year, month - 1, day);
    return isNaN(dt.getTime()) ? null : dt.toISOString();
  }

  // Helper method to debug form validation
  logFormErrors() {
    console.log('Form is invalid. Current errors:');
    Object.keys(this.form.controls).forEach(key => {
      const control = this.form.get(key);
      if (control?.invalid) {
        console.log(`${key}:`, control.errors);
      }
    });
  }

  // Helper method to check if a specific field is invalid
  isFieldInvalid(fieldName: string): boolean {
    const field = this.form.get(fieldName);
    return field ? field.invalid && field.touched : false;
  }
}
