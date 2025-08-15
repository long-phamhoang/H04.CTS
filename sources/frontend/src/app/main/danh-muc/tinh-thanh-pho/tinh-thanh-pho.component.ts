import { ListService, PagedResultDto } from '@abp/ng.core';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgbDateNativeAdapter, NgbDateAdapter } from '@ng-bootstrap/ng-bootstrap';
import { TinhThanhPhoDto } from '@app/proxy';
import { TinhThanhPhoService } from '@app/proxy/services/tinh-thanh-pho.service';
import { trangThaiOptions } from '@app/proxy/enums';

@Component({
  standalone: false,
  selector: 'app-tinh-thanh-pho',
  templateUrl: './tinh-thanh-pho.component.html',
  styleUrls: ['./tinh-thanh-pho.component.less'],
  providers: [
    ListService,
    { provide: NgbDateAdapter, useClass: NgbDateNativeAdapter }
  ],
})
export class TinhThanhPhoComponent implements OnInit {
  tinhThanhPho = { items: [], totalCount: 0 } as PagedResultDto<TinhThanhPhoDto>;

  selectedTinhThanhPho = {} as TinhThanhPhoDto; // declare selectedTinhThanhPho

  trangThaiOptions = trangThaiOptions;

  form: FormGroup;

  isModalOpen = false;

  searchValue: string = '';

  page = 1;
  pageSize = 10;
  fieldSort = '';

  @ViewChild('searchInput') searchInput: ElementRef;

  constructor(
    public readonly list: ListService,
    private tinhThanhPhoService: TinhThanhPhoService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService
  ) { 
  }

  ngOnInit() {
    this.list.maxResultCount = this.pageSize;
    this.list.page = this.page - 1;
    this.list.sortKey = this.fieldSort;
    const bookStreamCreator = (query) => this.tinhThanhPhoService.getList(query, this.searchInput.nativeElement.value);

    this.list.hookToQuery(bookStreamCreator).subscribe((response) => {
      this.tinhThanhPho = response;
    });
  }

  createTinhThanhPho() {
    this.selectedTinhThanhPho = {} as TinhThanhPhoDto; // reset the selected TinhThanhPho
    this.buildForm();
    this.isModalOpen = true;
  }

  editTinhThanhPho(id: string) {
    this.tinhThanhPhoService.get(id).subscribe((tinhThanhPho) => {
      this.selectedTinhThanhPho = tinhThanhPho;
      this.buildForm();
      this.isModalOpen = true;
    });
  }
  

  delete(id: string) {
    this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure').subscribe((status) => {
      if (status === Confirmation.Status.confirm) {
        this.tinhThanhPhoService.delete(id).subscribe(() => this.list.get());
      }
    });
  }

  buildForm() {
    this.form = this.fb.group({
      tenTinhThanhPho: [this.selectedTinhThanhPho.tenTinhThanhPho || '', Validators.required],
      maTinhThanhPho: [this.selectedTinhThanhPho.maTinhThanhPho || '', Validators.required],
      trangThai: [this.selectedTinhThanhPho.trangThai || ""],
      ghiChu: [this.selectedTinhThanhPho.ghiChu || ''],
    });
  }

  // change the save method
  save() {
    console.log('save');
    console.log(this.form.value);
    if (this.form.invalid) {
      return;
    }

    const request = this.selectedTinhThanhPho.id
      ? this.tinhThanhPhoService.update(this.selectedTinhThanhPho.id, this.form.value)
      : this.tinhThanhPhoService.create(this.form.value);

    request.subscribe(() => {
      this.isModalOpen = false;
      this.form.reset();
      this.list.get();
    });
  }

  search() {
    this.list.get();
  }

  onPageChange(event: any) {
    this.fieldSort = event.sortField;
    this.page = event.first / event.rows + 1;
    this.pageSize = event.rows;

    this.list.get();
  }
  
}
  