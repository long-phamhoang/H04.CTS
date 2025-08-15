import { ListService, PagedResultDto } from '@abp/ng.core';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgbDateNativeAdapter, NgbDateAdapter } from '@ng-bootstrap/ng-bootstrap';
import { TinhThanhPhoDto, XaPhuongDto } from '@app/proxy';
import { XaPhuongService } from '@app/proxy/services/xa-phuong.service';
import { trangThaiOptions } from '@app/proxy/enums';
import { TinhThanhPhoService } from '@app/proxy/services/tinh-thanh-pho.service';

@Component({
  standalone: false,
  selector: 'app-xa-phuong',
  templateUrl: './xa-phuong.component.html',
  styleUrls: ['./xa-phuong.component.less'],
  providers: [
    ListService,
    { provide: NgbDateAdapter, useClass: NgbDateNativeAdapter }
  ],
})
export class XaPhuongComponent implements OnInit {
  xaPhuong = { items: [], totalCount: 0 } as PagedResultDto<XaPhuongDto>;

  selectedXaPhuong = {} as XaPhuongDto; // declare selectedXaPhuong

  trangThaiOptions = trangThaiOptions;

  form: FormGroup;

  isModalOpen = false;

  tinhThanhPhoList = [] as PagedResultDto<TinhThanhPhoDto>;

  tinhThanhPhoId: string = null;

  page = 1;
  pageSize = 10;

  @ViewChild('searchInput') searchInput: ElementRef;

  constructor(
    public readonly list: ListService,
    private xaPhuongService: XaPhuongService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService,
    private tinhThanhPhoService: TinhThanhPhoService
  ) { 
    this.getListTinhThanhPho();
  }

  ngOnInit() {
    this.list.maxResultCount = this.pageSize;
    this.list.page = this.page - 1;
    const bookStreamCreator = (query) => this.xaPhuongService.getList(query, this.searchInput.nativeElement.value, this.tinhThanhPhoId);

    this.list.hookToQuery(bookStreamCreator).subscribe((response) => {
      this.xaPhuong = response;
    });
  }

  createXaPhuong() {
    this.selectedXaPhuong = {} as XaPhuongDto;
    this.buildForm();
    this.isModalOpen = true;
  }

  editXaPhuong(id: string) {
    this.xaPhuongService.get(id).subscribe((xaPhuong) => {
      this.selectedXaPhuong = xaPhuong;
      this.buildForm();
      this.isModalOpen = true;
    });
  }
  

  delete(id: string) {
    this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure').subscribe((status) => {
      if (status === Confirmation.Status.confirm) {
        this.xaPhuongService.delete(id).subscribe(() => this.list.get());
      }
    });
  }

  buildForm() {
    this.form = this.fb.group({
      tinhThanhPhoId: [this.selectedXaPhuong.tinhThanhPhoId || '', Validators.required],
      tenXaPhuong: [this.selectedXaPhuong.tenXaPhuong || '', Validators.required],
      maXaPhuong: [this.selectedXaPhuong.maXaPhuong || '', Validators.required],
      trangThai: [this.selectedXaPhuong.trangThai || ""],
      ghiChu: [this.selectedXaPhuong.ghiChu || ''],
    });
  }

  // change the save method
  save() {
    if (this.form.invalid) {
      return;
    }

    const request = this.selectedXaPhuong.id
      ? this.xaPhuongService.update(this.selectedXaPhuong.id, this.form.value)
      : this.xaPhuongService.create(this.form.value);

    request.subscribe(() => {
      this.isModalOpen = false;
      this.form.reset();
      this.list.get();
    });
  }
  //
  getListTinhThanhPho() {
    this.tinhThanhPhoService.getList({maxResultCount: 1000}).subscribe((response) => {
      this.tinhThanhPhoList = response;
    });
  }

  search() {
    console.log(this.tinhThanhPhoId, 'tinhThanhPhoId');
    console.log(this.searchInput.nativeElement.value, 'searchInput');
    this.list.get();
  }

  onPageChange(event: any) {
    this.page = event.first / event.rows + 1;
    this.pageSize = event.rows;
    this.list.get();
  }
}
