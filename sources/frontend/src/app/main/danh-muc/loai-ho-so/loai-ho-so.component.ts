import { ListService, PagedResultDto } from '@abp/ng.core';
import { Confirmation, ConfirmationService, ToasterService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { AbstractControl, AsyncValidatorFn, FormBuilder, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { TrangThai } from '@app/proxy/enums';
import { LoaiHoSoDto } from '@app/proxy/models';
import { LoaiHoSoService } from '@app/proxy/services';
import { ExportColumn } from '@app/shared/components/export-excel-dialog/export-excel-dialog.component';
import { NgbDateNativeAdapter, NgbDateAdapter } from '@ng-bootstrap/ng-bootstrap';
import { debounceTime, distinctUntilChanged, map, Observable, of, Subject, switchMap, takeUntil, timer } from 'rxjs';

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
  loaiHoSo = { items: [], totalCount: 0 } as PagedResultDto<LoaiHoSoDto>;
  isModalOpen = false;
  form: FormGroup;
  selectedLoaiHoSo = {} as LoaiHoSoDto;
  keySearch = '';
  statuses$ = [
    { value: TrangThai.HoatDong, label: '::Enum:TrangThai.HoatDong' },
    { value: TrangThai.KhongHoatDong, label: '::Enum:TrangThai.KhongHoatDong' }
  ];
  private searchSubject = new Subject<string>();
  rows = 10;
  skipCount = 0;
  TrangThai = TrangThai;
  exportVisible = false;
  exportColumns: ExportColumn[] = [
    { field: 'maLoaiHoSo', header: 'Mã loại hồ sơ' },
    { field: 'tenLoaiHoSo', header: 'Tên loại hồ sơ' },
    { field: 'trangThai', header: 'Trạng thái' },
    { field: 'ghiChu', header: 'Ghi chú' },
  ];
  exportData: any[] = [];


  constructor(
    public list: ListService,
    private readonly loaiHoSoService: LoaiHoSoService,
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
      this.loaiHoSoService.getFilterList({
        keyword: keyword || ''
      }).subscribe(res => this.loaiHoSo = res)
    });
  }

  loadData() {
    this.loaiHoSoService.getFilterList({
      keyword: this.keySearch || '',
      skipCount: this.skipCount,
      maxResultCount: this.rows
    }).subscribe(res => this.loaiHoSo = res);
  }

  createLoaiHoSo() {
    this.selectedLoaiHoSo = {} as LoaiHoSoDto;
    this.buildForm();
    this.isModalOpen = true;
  }

  editLoaiHoSo(id: number) {
    this.loaiHoSoService.get(id).subscribe((response) => {
      this.selectedLoaiHoSo = response;
      this.buildForm();
      this.isModalOpen = true;
    })
  }

  deleteLoaiHoSo(id: number) {
    this.confirmationService.warn('Cts::AreYouSureDelete', 'Cts::AreYouSure').subscribe((status) => {
      if(status === Confirmation.Status.confirm) {
        this.loaiHoSoService.softDelete(id).subscribe(() => {
          this.toaster.success('Cts::SuccessfullyDeleted', 'Thông báo');
          this.loadData();
        });
      }
    })
  }

  maLoaiHoSoUniqueValidator():AsyncValidatorFn {
    return (control: AbstractControl): Observable<ValidationErrors | null> => {
      const ma = control.value;
      if(!ma || ma === this.selectedLoaiHoSo.maLoaiHoSo) {
        return of(null);
      }
      return timer(500).pipe(
        switchMap(() =>
          this.loaiHoSoService.isExistsMaLoaiHoSo(ma, this.selectedLoaiHoSo?.id).pipe(
            map(isDuplicate => (isDuplicate ? {maLoaiHoSoExists: true} : null))
          )
        )
      );
    }
  }

  buildForm() {
    this.form = this.fb.group({
      maLoaiHoSo: [this.selectedLoaiHoSo.maLoaiHoSo || '', Validators.required, [this.maLoaiHoSoUniqueValidator()]],
      tenLoaiHoSo: [this.selectedLoaiHoSo.tenLoaiHoSo || '', Validators.required],
      trangThai: [this.selectedLoaiHoSo.trangThai || null, Validators.required],
      ghiChu: [this.selectedLoaiHoSo.ghiChu || ''],
    });


  }

  save() {
    if(this.form.invalid) {
      return;
    }

    const request = this.selectedLoaiHoSo.id
      ? this.loaiHoSoService.update(this.selectedLoaiHoSo.id, this.form.value)
      : this.loaiHoSoService.create(this.form.value);

    request.subscribe(() => {
      this.isModalOpen = false;
      this.form.reset();
      this.loadData();
      this.toaster.success(this.selectedLoaiHoSo.id
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

  onExported() {
    // const mapTrangThai = (v: number | null | undefined) => this.getTrangThai(v);
    this.loaiHoSoService.getFilterList({
      keyword: this.keySearch || '',
      skipCount: 0,
      maxResultCount: this.loaiHoSo.totalCount
    }).subscribe(res => {
      this.exportData = (res.items || []).map(x => ({
        ...x,
        trangThai: x.trangThai === TrangThai.HoatDong ? 'Hoạt động' : 'Không hoạt động'
      }));
      this.exportVisible = true;
    });
  }
}