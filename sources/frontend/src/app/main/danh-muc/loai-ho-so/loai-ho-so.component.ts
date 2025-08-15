import { ListService, PagedResultDto } from '@abp/ng.core';
import { Confirmation, ConfirmationService, ToasterService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TrangThai } from '@app/proxy/enums';
import { LoaiHoSoDto } from '@app/proxy/models';
import { LoaiHoSoService } from '@app/proxy/services';
import { NgbDateNativeAdapter, NgbDateAdapter } from '@ng-bootstrap/ng-bootstrap';
import { debounceTime, distinctUntilChanged, Subject, switchMap, takeUntil } from 'rxjs';

@Component({
  selector: 'app-loai-ho-so',
  templateUrl: './loai-ho-so.component.html',
  styleUrls: ['./loai-ho-so.component.less'],
  standalone: false,
  providers: [
    ListService,
    { provide: NgbDateAdapter, useClass: NgbDateNativeAdapter },
  ]
})

export class LoaiHoSoComponent implements OnInit {
  documentType = { items: [], totalCount: 0 } as PagedResultDto<LoaiHoSoDto>;
  isModalOpen = false;
  form: FormGroup;
  selectedDocumentType = {} as LoaiHoSoDto;
  keySearch = '';
  statuses$ = [
    { value: TrangThai.HoatDong, label: '::Enum:TrangThai.HoatDong' },
    { value: TrangThai.KhongHoatDong, label: '::Enum:TrangThai.KhongHoatDong' }
  ];
  private searchSubject = new Subject<string>();
  rows = 10;
  skipCount = 0;


  constructor(
    public list: ListService,
    private readonly documentTypeService: LoaiHoSoService,
    private fb: FormBuilder,
    private confirmationService: ConfirmationService,
    private toaster: ToasterService
  ) {
  }

  ngOnInit(): void {
    // const documentTypeStreamCreator = (query) => this.documentTypeService.getList(query);
    // this.list.hookToQuery(documentTypeStreamCreator).subscribe((response) => {
    //   this.documentType = response;
    // });
    this.loadData();
    this.searchSubject.pipe(
      debounceTime(500),
      distinctUntilChanged()
    ).subscribe(keyword => {
      this.documentTypeService.getFilterList({
        keyword: keyword || ''
      }).subscribe(res => this.documentType = res)
    });
  }

  loadData() {
    this.documentTypeService.getFilterList({
      keyword: this.keySearch || '',
      skipCount: this.skipCount,
      maxResultCount: this.rows
    }).subscribe(res => this.documentType = res);
  }

  createDocumentType() {
    this.selectedDocumentType = {} as LoaiHoSoDto;
    this.buildForm();
    this.isModalOpen = true;
  }

  editDocumentType(id: number) {
    this.documentTypeService.get(id).subscribe((response) => {
      this.selectedDocumentType = response;
      this.buildForm();
      this.isModalOpen = true;
    })
  }

  deleteDocumentType(id: number) {
    this.confirmationService.warn('Cts::AreYouSureDelete', 'Cts::AreYouSure').subscribe((status) => {
      if(status === Confirmation.Status.confirm) {
        this.documentTypeService.softDelete(id).subscribe(() => {
          this.toaster.success('Cts::SuccessfullyDeleted', 'Thông báo');
          this.loadData();
        });
      }
    })
  }

  buildForm() {
    this.form = this.fb.group({
      maLoaiHoSo: [this.selectedDocumentType.maLoaiHoSo || '', Validators.required],
      tenLoaiHoSo: [this.selectedDocumentType.tenLoaiHoSo || '', Validators.required],
      trangThai: [this.selectedDocumentType.trangThai || null, Validators.required],
      ghiChu: [this.selectedDocumentType.ghiChu || ''],
    })
  }

  save() {
    if(this.form.invalid) {
      return;
    }

    const request = this.selectedDocumentType.id
      ? this.documentTypeService.update(this.selectedDocumentType.id, this.form.value)
      : this.documentTypeService.create(this.form.value);

    request.subscribe(() => {
      this.isModalOpen = false;
      this.form.reset();
      this.loadData();
      this.toaster.success(this.selectedDocumentType.id
        ? 'Cts::UpdatedSuccessfully'
        : 'Cts::CreatedSuccessfully', 'Thông báo');
    })
  }

  search() {
    this.searchSubject.next(this.keySearch);
  }

  onPageChange(event: any) {
    this.skipCount = event.page * event.rows;
    this.rows = event.rows;
    this.loadData();
  }
}