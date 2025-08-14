import { ListService, PagedResultDto } from '@abp/ng.core';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgbDateNativeAdapter, NgbDateAdapter } from '@ng-bootstrap/ng-bootstrap';
import { ToChucDto, ToChucService, trangThaiOptions } from '@app/proxy';
import { mapEnumToOptions } from '@abp/ng.core';
@Component({
  standalone: false,
  selector: 'app-to-chuc',
  templateUrl: './to-chuc.component.html',
  styleUrls: ['./to-chuc.component.less'],
  providers: [
    ListService,
    { provide: NgbDateAdapter, useClass: NgbDateNativeAdapter }
  ],
})
export class ToChucComponent implements OnInit {
  toChuc = { items: [], totalCount: 0 } as PagedResultDto<ToChucDto>;

  selectedToChuc = {} as ToChucDto; // declare selectedToChuc

  form: FormGroup;

  trangThaiOptions = trangThaiOptions;

  isModalOpen = false;

  constructor(
    public readonly list: ListService,
    private toChucService: ToChucService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService // inject the ConfirmationService
  ) { }

  ngOnInit() {
    const bookStreamCreator = (query) => this.toChucService.getList(query);

    this.list.hookToQuery(bookStreamCreator).subscribe((response) => {
      this.toChuc = response;
    });
  }

  createToChuc() {
    this.selectedToChuc = {} as ToChucDto; // reset the selected toChuc
    this.buildForm();
    this.isModalOpen = true;
  }

  editToChuc(id: number) {
    this.toChucService.get(id).subscribe((toChuc) => {
      this.selectedToChuc = toChuc;
      this.buildForm();
      this.isModalOpen = true;
    });
  }

  delete(id: number) {
    this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure').subscribe((status) => {
      if (status === Confirmation.Status.confirm) {
        this.toChucService.delete(id).subscribe(() => this.list.get());
      }
    });
  }

  buildForm() {
    this.form = this.fb.group({
      tenToChuc: [this.selectedToChuc.tenToChuc || '', Validators.required],
      trangThai: [this.selectedToChuc.trangThai || null, Validators.required],
    });
  }

  // change the save method
  save() {
    if (this.form.invalid) {
      return;
    }

    const request = this.selectedToChuc.id
      ? this.toChucService.update(this.selectedToChuc.id, this.form.value)
      : this.toChucService.create(this.form.value);

    request.subscribe(() => {
      this.isModalOpen = false;
      this.form.reset();
      this.list.get();
    });
  }
}
