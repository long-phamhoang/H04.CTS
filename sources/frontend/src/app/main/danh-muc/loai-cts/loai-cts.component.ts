import { ListService, PagedResultDto } from '@abp/ng.core';
import { Confirmation, ConfirmationService, ToasterService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { AbstractControl, AsyncValidatorFn, FormBuilder, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { LoaiCTSDto, TrangThai } from '@app/proxy';
import { LoaiCTSService } from '@app/proxy/services';
import { NgbDateNativeAdapter, NgbDateAdapter } from '@ng-bootstrap/ng-bootstrap';
import { debounceTime, distinctUntilChanged, map, Observable, of, Subject, switchMap, takeUntil, timer } from 'rxjs';
import { ExportColumn, ExportExcelDialogComponent } from '@app/shared/components/export-excel-dialog/export-excel-dialog.component';

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
  loaiCTS = {
    items: [],
    totalCount: 0
  } as PagedResultDto<LoaiCTSDto>;
  selectedLoaiCTS = {} as LoaiCTSDto;
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
  TrangThai = TrangThai;
  exportVisible = false;
	exportColumns: ExportColumn[] = [
		{ field: 'maLoaiCTS', header: 'Mã loại chứng thư số' },
		{ field: 'tenLoaiCTS', header: 'Tên loại chứng thư số' },
		{ field: 'trangThai', header: 'Trạng thái' },
		{ field: 'ghiChu', header: 'Ghi chú' },
	];
	exportData: any[] = [];

  constructor(
    public readonly list: ListService,
    private loaiCtsService: LoaiCTSService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService,
    private toaster: ToasterService
  ) {

  }

  ngOnInit() {
    this.loadData();
    this.searchSubject.pipe(
        debounceTime(500),
        distinctUntilChanged(),
      ).subscribe(keyword => {
      this.loaiCtsService.getFilterList({
        keyword: keyword || ''
      }).subscribe(res => this.loaiCTS = res)
    });
  }

  loadData() {
    this.loaiCtsService.getFilterList({
      keyword: this.keySearch || '',
      skipCount: this.skipCount,
      maxResultCount: this.rows
    }).subscribe((res) => {
      this.loaiCTS = res;
    });
  }

  createLoaiCTS() {
    this.selectedLoaiCTS = {} as LoaiCTSDto;
    this.buildForm();
    this.isModalOpen = true;
  }

  editLoaiCTS(id: number) {
    this.loaiCtsService.get(id).subscribe((digitalType) => {
      this.selectedLoaiCTS = digitalType;
      this.buildForm();
      this.isModalOpen = true;
    })
  }

  deleteLoaiCTS(id: number) {
    this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure').subscribe((status) => {
      if (status === Confirmation.Status.confirm) {
        this.loaiCtsService.softDelete(id).subscribe(() => {
          this.loadData();
        });
      }
    });
  }

  maLoaiCTSUniqueValidator():AsyncValidatorFn {
    return (control: AbstractControl): Observable<ValidationErrors | null> => {
      const ma = control.value;
      if(!ma || ma === this.selectedLoaiCTS.maLoaiCTS) {
        return of(null);
      }
      return timer(500).pipe(
        switchMap(() =>
          this.loaiCtsService.isExistsMaLoaiCTS(ma, this.selectedLoaiCTS?.id).pipe(
            map(isDuplicate => (isDuplicate ? {maLoaiCTSExists: true} : null))
          )
        )
      );
    }
  }

  buildForm() {
    this.form = this.fb.group({
      maLoaiCTS: [this.selectedLoaiCTS.maLoaiCTS || '', Validators.required, [this.maLoaiCTSUniqueValidator()]],
      tenLoaiCTS: [this.selectedLoaiCTS.tenLoaiCTS || '', Validators.required],
      trangThai: [this.selectedLoaiCTS.trangThai || null, Validators.required],
      ghiChu: [this.selectedLoaiCTS.ghiChu || '']
    });
  }

  save() {
    if(this.form.invalid) {
      return;
    }

    const request = this.selectedLoaiCTS.id
      ? this.loaiCtsService.update(this.selectedLoaiCTS.id, this.form.value)
      : this.loaiCtsService.create(this.form.value);
    
    request.subscribe(() => {
      this.isModalOpen = false;
      this.form.reset();
      this.loadData();
      this.toaster.success(this.selectedLoaiCTS.id
        ? 'Cts::UpdatedSuccessfully'
        : 'Cts::CreatedSuccessfully', 'Thông báo');
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

  onExported() {
    this.loaiCtsService.getFilterList({
      keyword: this.keySearch || '',
      skipCount: 0,
      maxResultCount: this.loaiCTS.totalCount
    }).subscribe(res => {
      this.exportData = (res.items || []).map(x => ({
        ...x,
        trangThai: x.trangThai === TrangThai.HoatDong ? 'Hoạt động' : 'Không hoạt động'
      }));
      this.exportVisible = true;
    });
  }
}