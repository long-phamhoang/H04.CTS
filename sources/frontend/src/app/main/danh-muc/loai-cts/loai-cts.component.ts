import { ListService, PagedResultDto } from '@abp/ng.core';
import { Confirmation, ConfirmationService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LoaiCTSDto, TrangThai } from '@app/proxy';
import { LoaiCTSService } from '@app/proxy/services';
import { NgbDateNativeAdapter, NgbDateAdapter } from '@ng-bootstrap/ng-bootstrap';
import { debounceTime, distinctUntilChanged, Subject, takeUntil } from 'rxjs';


@Component({
  selector: 'app-loai-cts',
  standalone: false,
  templateUrl: './loai-cts.component.html',
  styleUrls: ['./loai-cts.component.less'],
  providers: [
    ListService,
    { provide: NgbDateAdapter, useClass: NgbDateNativeAdapter }
  ]
})

export class LoaiCTSComponent implements OnInit {
  digitalCertiType = {
    items: [],
    totalCount: 0
  } as PagedResultDto<LoaiCTSDto>;
  selectedDigitalCerti = {} as LoaiCTSDto;
  form: FormGroup;
  isModalOpen = false;
  statuses$ = [
    { value: TrangThai.HoatDong, label: '::Enum:TrangThai.HoatDong' },
    { value: TrangThai.KhongHoatDong, label: '::Enum:TrangThai.KhongHoatDong' }
  ];
  currentPage = 0;
  keySearch = '';
  private searchSubject = new Subject<string>();
  rows = 10;
  skipCount = 0;

  constructor(
    public readonly list: ListService,
    private loaiCtsService: LoaiCTSService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService
  ) {

  }

  async ngOnInit(): Promise<void> {
    // const digitalCertiTypeStreamCreator = (query) => this.loaiCtsService.getFilterList(query);

    // this.list.hookToQuery(digitalCertiTypeStreamCreator).subscribe((response) => {
    //   this.digitalCertiType = response;
    // });
    await this.loadData();
    this.searchSubject.pipe(
        debounceTime(500),
        distinctUntilChanged(),
      ).subscribe(keyword => {
      this.loaiCtsService.getFilterList({
        keyword: keyword || ''
      }).subscribe(res => this.digitalCertiType = res)
    });
  }

  async loadData() {
    const res = await this.loaiCtsService.getFilterList({
      keyword: this.keySearch || '',
      skipCount: this.skipCount,
      maxResultCount: this.rows
    }).toPromise();
    
    this.digitalCertiType.items = res.items;
    this.digitalCertiType.totalCount = res.totalCount;
  }

  createDigitalCertiType() {
    this.selectedDigitalCerti = {} as LoaiCTSDto;
    this.buildForm();
    this.isModalOpen = true;
  }

  editDigitalCertiType(id: number) {
    this.loaiCtsService.get(id).subscribe((digitalType) => {
      this.selectedDigitalCerti = digitalType;
      this.buildForm();
      this.isModalOpen = true;
    })
  }

  deleteDigitalCertiType(id: number) {
    this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure').subscribe((status) => {
      if (status === Confirmation.Status.confirm) {
        this.loaiCtsService.softDelete(id).subscribe(() => {
          this.loadData();
        });
      }
    });
  }

  buildForm() {
    this.form = this.fb.group({
      maLoaiCTS: [this.selectedDigitalCerti.maLoaiCTS || '', Validators.required],
      tenLoaiCTS: [this.selectedDigitalCerti.tenLoaiCTS || '', Validators.required],
      trangThai: [this.selectedDigitalCerti.trangThai || null, Validators.required],
      ghiChu: [this.selectedDigitalCerti.ghiChu || '']
    });
  }

  save() {
    if(this.form.invalid) {
      return;
    }

    const request = this.selectedDigitalCerti.id
      ? this.loaiCtsService.update(this.selectedDigitalCerti.id, this.form.value)
      : this.loaiCtsService.create(this.form.value);
    
    request.subscribe(() => {
      this.isModalOpen = false;
      this.form.reset();
      this.loadData();
    })
  }

  onPageChange(event: any) {
    this.skipCount = event.page * event.rows;
    console.log(event);
    this.rows = event.rows;
    this.loadData();
  }

  search() {
    this.searchSubject.next(this.keySearch);
  }

}