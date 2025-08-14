import { ListService, PagedResultDto } from '@abp/ng.core';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgbDateNativeAdapter, NgbDateAdapter } from '@ng-bootstrap/ng-bootstrap';
import { CapCoQuanDto, CapCoQuanService, trangThaiOptions } from '@app/proxy';

@Component({
  standalone: false,
  selector: 'app-cap-co-quan',
  templateUrl: './cap-co-quan.component.html',
  styleUrls: ['./cap-co-quan.component.less'],
  providers: [
    ListService,
    { provide: NgbDateAdapter, useClass: NgbDateNativeAdapter }
  ],
})
export class CapCoQuanComponent implements OnInit {
  CapCoQuan = { items: [], totalCount: 0 } as PagedResultDto<CapCoQuanDto>;

  selectedCapCoQuan = {} as CapCoQuanDto; // declare selectedCapCoQuan

  form: FormGroup;

  trangThaiOptions = trangThaiOptions;

  isModalOpen = false;

  constructor(
    public readonly list: ListService,
    private CapCoQuanService: CapCoQuanService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService // inject the ConfirmationService
  ) { }

  ngOnInit() {
    const capCoQuanStreamCreator = (query) => this.CapCoQuanService.getList(query);

    this.list.hookToQuery(capCoQuanStreamCreator).subscribe((response) => {
      this.CapCoQuan = response;
    });
  }

  createCapCoQuan() {
    this.selectedCapCoQuan = {} as CapCoQuanDto; // reset the selected CapCoQuan
    this.buildForm();
    this.isModalOpen = true;
  }

  editCapCoQuan(id: number) {
    this.CapCoQuanService.get(id).subscribe((CapCoQuan) => {
      this.selectedCapCoQuan = CapCoQuan;
      this.buildForm();
      this.isModalOpen = true;
    });
  }

  delete(id: number) {
    this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure').subscribe((status) => {
      if (status === Confirmation.Status.confirm) {
        this.CapCoQuanService.delete(id).subscribe(() => this.list.get());
      }
    });
  }

  buildForm() {
    this.form = this.fb.group({
      tenCapCoQuan: [this.selectedCapCoQuan.tenCapCoQuan || '', Validators.required],
      maCapCoQuan: [this.selectedCapCoQuan.maCapCoQuan || '', Validators.required],
      trangThai: [this.selectedCapCoQuan.trangThai || null, Validators.required],
    });
  }

  // change the save method
  save() {
    if (this.form.invalid) {
      return;
    }

    const request = this.selectedCapCoQuan.id
      ? this.CapCoQuanService.update(this.selectedCapCoQuan.id, this.form.value)
      : this.CapCoQuanService.create(this.form.value);

    request.subscribe(() => {
      this.isModalOpen = false;
      this.form.reset();
      this.list.get();
    });
  }
}
