import { ListService, PagedResultDto } from '@abp/ng.core';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormBuilder, Validators, AbstractControl } from '@angular/forms';
import { NgbDateNativeAdapter, NgbDateAdapter } from '@ng-bootstrap/ng-bootstrap';
import { NguoiTiepNhanDto, NguoiTiepNhanService, trangThaiOptions, NoiCapCCCDService, NoiCapCCCDDto, ToChucService, ToChucDto } from '@app/proxy';
import { Subject, debounceTime, takeUntil, distinctUntilChanged } from 'rxjs';

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
export class NguoiTiepNhanComponent implements OnInit, OnDestroy {
  nguoiTiepNhan = { items: [], totalCount: 0 } as PagedResultDto<NguoiTiepNhanDto>;

  selectedNguoiTiepNhan = {} as NguoiTiepNhanDto; // declare selectedNguoiTiepNhan

  form: FormGroup;

  trangThaiOptions = trangThaiOptions;

  isModalOpen = false;

  noiCapCCCDs: NoiCapCCCDDto[] = [];
  organizations: ToChucDto[] = [];

	pageSize = 10;

  keyword: string = '';

  // CCCD validation properties
  private cccdCheckSubject = new Subject<string>();
  private destroy$ = new Subject<void>();
  cccdExists = false;

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

    // Initialize form first
    this.buildForm();

    // Setup CCCD validation with debounce
    this.setupCccdValidation();

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

  ngOnDestroy() {
    this.destroy$.next();
    this.destroy$.complete();
  }

  private setupCccdValidation() {
    this.cccdCheckSubject
      .pipe(
        debounceTime(500), // Wait 500ms after user stops typing
        distinctUntilChanged(), // Only emit if value changed
        takeUntil(this.destroy$)
      )
      .subscribe(cccd => {
        if (cccd && cccd.length >= 3) { // Only check if CCCD has at least 3 characters
          this.checkCccdExists(cccd);
        } else {
          this.cccdExists = false;
        }
      });
  }

  private checkCccdExists(cccd: string) {
    // Skip check if editing existing record with same CCCD
    if (this.selectedNguoiTiepNhan.id && this.selectedNguoiTiepNhan.cccd === cccd) {
      this.cccdExists = false;
      return;
    }

    this.nguoiTiepNhanService.checkExist({ field: 'cccd', value: cccd })
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: (exists) => {
          this.cccdExists = exists;
          
          // Update form validation
          const cccdControl = this.form.get('cccd');
          if (cccdControl) {
            if (exists) {
              cccdControl.setErrors({ ...cccdControl.errors, cccdExists: true });
            } else {
              // Remove cccdExists error but keep other validations
              const errors = cccdControl.errors;
              if (errors) {
                delete errors.cccdExists;
                cccdControl.setErrors(Object.keys(errors).length > 0 ? errors : null);
              }
            }
          }
        },
        error: (error) => {
          console.error('Error checking CCCD:', error);
        }
      });
  }

  onCccdInput(event: any) {
    const cccd = event.target.value;
    this.cccdCheckSubject.next(cccd);
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
    const formatDateToInput = (date: any) => {
      if (!date) return null;
      const d = new Date(date);
      const year = d.getFullYear();
      const month = String(d.getMonth() + 1).padStart(2, '0');
      const day = String(d.getDate()).padStart(2, '0');
      return `${year}-${month}-${day}`;
    };

    // Reset CCCD validation state
    this.cccdExists = false;

    this.form = this.fb.group({
      organizationId: [this.selectedNguoiTiepNhan.organizationId || null],
      fullName: [this.selectedNguoiTiepNhan.fullName || '', Validators.required],
      cccd: [this.selectedNguoiTiepNhan.cccd || null, Validators.required],
      dateOfIssue: [formatDateToInput(this.selectedNguoiTiepNhan.dateOfIssue) || null, Validators.required],
      noiCapCCCDId: [this.selectedNguoiTiepNhan.noiCapCCCDId || null, Validators.required],
      position: [this.selectedNguoiTiepNhan.position || null, Validators.required],
      phone: [this.selectedNguoiTiepNhan.phone || null, Validators.required],
      email: [this.selectedNguoiTiepNhan.email || null, Validators.required],
      submissionAddress: [this.selectedNguoiTiepNhan.submissionAddress || null, Validators.required],
      province: [this.selectedNguoiTiepNhan.province || null, Validators.required],
      ward: [this.selectedNguoiTiepNhan.ward || null, Validators.required],
      isDefault: [this.selectedNguoiTiepNhan.isDefault ?? false],
    });
  }

  // change the save method
  save() {
    if (this.form.invalid || this.cccdExists) {
      this.logFormErrors();
      return;
    }

    const raw = this.form.value as any;

    const toNullableNumber = (v: any) => {
      if (v === null || v === undefined || v === '') return null;
      const n = typeof v === 'number' ? v : Number(v);
      return Number.isNaN(n) ? null : n;
    };

    if (raw.dateOfIssue && !(raw.dateOfIssue instanceof Date)) {
      raw.dateOfIssue = new Date(raw.dateOfIssue);
    }

    if (raw.dateOfIssue) {
      raw.dateOfIssue = raw.dateOfIssue.toISOString().split('T')[0];
    }

    // Build DTO with proper null handling
    const dto: any = {
      fullName: raw.fullName,
      cccd: raw.cccd,
      dateOfIssue: raw.dateOfIssue,
      position: raw.position,
      phone: raw.phone,
      email: raw.email,
      submissionAddress: raw.submissionAddress,
      province: raw.province,
      ward: raw.ward,
      isDefault: typeof raw.isDefault === 'string' ? raw.isDefault === 'true' : !!raw.isDefault,
    };

    // Only include organizationId if it has a value
    if (raw.organizationId !== null && raw.organizationId !== undefined && raw.organizationId !== '') {
      dto.organizationId = toNullableNumber(raw.organizationId);
    }

    // Only include noiCapCCCDId if it has a value
    if (raw.noiCapCCCDId !== null && raw.noiCapCCCDId !== undefined && raw.noiCapCCCDId !== '') {
      dto.noiCapCCCDId = toNullableNumber(raw.noiCapCCCDId);
    }

    const request = this.selectedNguoiTiepNhan.id
      ? this.nguoiTiepNhanService.update(this.selectedNguoiTiepNhan.id, dto)
      : this.nguoiTiepNhanService.create(dto);

    request.subscribe(() => {
      this.isModalOpen = false;
      this.form.reset();
      this.reload();
    });
  }
  // Helper method to debug form validation
  logFormErrors() {
    console.log('Form is invalid. Current errors:');
    if (this.cccdExists) {
      console.log('CCCD already exists in the system');
    }
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

  // Helper method to check if CCCD field is invalid (including duplicate check)
  isCccdInvalid(): boolean {
    const cccdControl = this.form.get('cccd');
    if (!cccdControl) return false;
    
    return (cccdControl.invalid && cccdControl.touched) || this.cccdExists;
  }

  closeModal() {
    this.isModalOpen = false;
  }

  // Toggle isDefault status directly from table
  toggleIsDefault(row: NguoiTiepNhanDto, newValue: boolean) {
    // Store original value to revert if update fails
    const originalValue = row.isDefault;
    
    // Optimistically update UI first
    row.isDefault = newValue;
    
    // Create update DTO with only the isDefault field and other required fields
    const updateDto: any = {
      fullName: row.fullName,
      cccd: row.cccd,
      dateOfIssue: row.dateOfIssue,
      position: row.position,
      phone: row.phone,
      email: row.email,
      submissionAddress: row.submissionAddress,
      province: row.province,
      ward: row.ward,
      isDefault: newValue,
    };

    // Only include organizationId if it has a value
    if (row.organizationId !== null && row.organizationId !== undefined) {
      updateDto.organizationId = row.organizationId;
    }

    // Only include noiCapCCCDId if it has a value
    if (row.noiCapCCCDId !== null && row.noiCapCCCDId !== undefined && row.noiCapCCCDId !== 0) {
      updateDto.noiCapCCCDId = row.noiCapCCCDId;
    }

    // Call update API
    this.nguoiTiepNhanService.update(row.id, updateDto).subscribe({
      next: () => {
        // Success - no need to reload, UI is already updated
        console.log('isDefault updated successfully');
      },
      error: (error) => {
        console.error('Error updating isDefault:', error);
        // Revert the toggle if update fails
        row.isDefault = originalValue;
        // You can add a toast notification here
      }
    });
  }
}
