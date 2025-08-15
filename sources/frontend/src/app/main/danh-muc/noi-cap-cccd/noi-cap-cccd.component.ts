import { ListService, PagedResultDto } from '@abp/ng.core';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgbDateNativeAdapter, NgbDateAdapter } from '@ng-bootstrap/ng-bootstrap';
import { NoiCapCCCDDto, NoiCapCCCDService, trangThaiOptions } from '@app/proxy';

@Component({
  standalone: false,
  selector: 'app-noi-cap-cccd',
  templateUrl: './noi-cap-cccd.component.html',
  styleUrls: ['./noi-cap-cccd.component.less'],
  providers: [
    ListService,
    { provide: NgbDateAdapter, useClass: NgbDateNativeAdapter }
  ],
})
export class NoiCapCCCDComponent implements OnInit {
  noiCapCCCD = { items: [], totalCount: 0 } as PagedResultDto<NoiCapCCCDDto>;

  selectedNoiCapCCCD = {} as NoiCapCCCDDto; // declare selectedNoiCapCCCD

  form: FormGroup;

  trangThaiOptions = trangThaiOptions;

  isModalOpen = false;

	pageSize = 10;

  keyword: string = '';

  constructor(
    public readonly list: ListService,
    private noiCapCCCDService: NoiCapCCCDService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService // inject the ConfirmationService
  ) { }

  ngOnInit() {
    // initial load
    // this.reload();
  }

  createNoiCapCCCD() {
    this.selectedNoiCapCCCD = {} as NoiCapCCCDDto; // reset the selected NoiCapCCCD
    this.buildForm();
    this.isModalOpen = true;
  }

  editNoiCapCCCD(id: number) {
    this.noiCapCCCDService.get(id).subscribe((noiCapCCCD) => {
      this.selectedNoiCapCCCD = noiCapCCCD;
      this.buildForm();
      this.isModalOpen = true;
    });
  }

  delete(id: number) {
    this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure').subscribe((status) => {
      if (status === Confirmation.Status.confirm) {
        this.noiCapCCCDService.delete(id).subscribe(() => this.reload());
      }
    });
  }

  buildForm() {
    this.form = this.fb.group({
      name: [this.selectedNoiCapCCCD.name || '', Validators.required],
      code: [this.selectedNoiCapCCCD.code || null, Validators.required],
      abbreviation: [this.selectedNoiCapCCCD.abbreviation || null, Validators.required],
      address: [this.selectedNoiCapCCCD.address || null, Validators.required],
      province: [this.selectedNoiCapCCCD.province || null, Validators.required],
      note: [this.selectedNoiCapCCCD.note || null, Validators.required],
      isActive: [this.selectedNoiCapCCCD.isActive || null, Validators.required],
    });
  }

  // change the save method
  save() {
    if (this.form.invalid) {
      this.logFormErrors();
      return;
    }

    const request = this.selectedNoiCapCCCD.id
      ? this.noiCapCCCDService.update(this.selectedNoiCapCCCD.id, this.form.value)
      : this.noiCapCCCDService.create(this.form.value);

    request.subscribe(() => {
      this.isModalOpen = false;
      this.form.reset();
      this.reload();
    });
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

	loadNoiCapCCCD(event: any) {
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
		this.noiCapCCCDService
			.getList({ keyword: this.keyword, skipCount: first, maxResultCount: rows, sorting })
			.subscribe((response) => (this.noiCapCCCD = response));
	}

  onSearch() {
    this.reload({ first: 0, rows: this.pageSize });
  }
}
