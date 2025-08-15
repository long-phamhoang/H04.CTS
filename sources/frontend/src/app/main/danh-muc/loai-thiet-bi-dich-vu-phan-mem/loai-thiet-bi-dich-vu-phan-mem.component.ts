import { ListService, PagedResultDto } from '@abp/ng.core';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgbDateNativeAdapter, NgbDateAdapter } from '@ng-bootstrap/ng-bootstrap';
import { LoaiThietBiDichVuPhanMemDto, LoaiThietBiDichVuPhanMemService, trangThaiOptions } from '@app/proxy';
import { TrangThai } from 'src/app/proxy/enums';
import { Subject } from 'rxjs';
import { debounceTime } from 'rxjs/operators';

@Component({
   standalone: false,
   selector: 'app-loai-thiet-bi-dich-vu-phan-mem',
   templateUrl: './loai-thiet-bi-dich-vu-phan-mem.component.html', 
   styleUrls: ['./loai-thiet-bi-dich-vu-phan-mem.component.less'],
   providers: [
     ListService,
     { provide: NgbDateAdapter, useClass: NgbDateNativeAdapter  }
   ],
})
export class LoaiThietBiDichVuPhanMemComponent implements OnInit {
  loaiThietBiPhanMem = { items: [], totalCount: 0 } as PagedResultDto<LoaiThietBiDichVuPhanMemDto>;
  selectedLoaiThietBiPhanMem = {} as LoaiThietBiDichVuPhanMemDto; // declare selectedLoaiThietBiPhanMem
  trangthaienums = TrangThai; 
  // search 
  searchValue = '';
  searchSubject = new Subject<string>();  
  originalItems = [];
  // paging
  page = 0;
  limit = 5;
  sorting = ''; // Có thể là tên cột hoặc kiểu sort bạn muốn

  // Form for creating/editing LoaiThietBiPhanMem
  form: FormGroup;
  trangThaiOptions = trangThaiOptions;
  isModalOpen = false;
  constructor(
    public readonly list: ListService,
    private loaiThietBiPhanMemService: LoaiThietBiDichVuPhanMemService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService // inject the ConfirmationService
  ) { }

  ngOnInit() {
    const bookStreamCreator = (query) => this.loaiThietBiPhanMemService.getList(query);
    this.list.hookToQuery(bookStreamCreator).subscribe((response) => {
      this.loaiThietBiPhanMem = response;
      this.originalItems = response.items; // Gán lại dữ liệu gốc mỗi lần load trang
      this.applySearch(this.searchValue); // Apply search nếu có từ khóa 
    });

    this.searchSubject.pipe( 
    debounceTime(500)       // search debounce time 500ms
    ).subscribe(value => {
      this.searchValue = value;
      this.applySearch(value);
    });
  }
  
  applySearch(value: string) {
    var filtered = [];
    if (!value || value.trim() === '') {
      filtered = [...this.originalItems]; // Hiển thị lại toàn bộ dữ liệu
    } else {
      filtered = this.originalItems.filter(item =>
        (item.tenLoaiThietBiDichVuPhanMem?.toLowerCase().includes(value.toLowerCase()) ||
        item.maLoaiThietBiDichVuPhanMem?.toLowerCase().includes(value.toLowerCase()))
      );
    }
    // Phân trang trên client
    const start = this.page * this.limit;
    const end = start + this.limit;
    this.loaiThietBiPhanMem.items = filtered.slice(start, end);
    this.loaiThietBiPhanMem.totalCount = filtered.length;
  }
  onSearchChange(value: string) {
    this.searchSubject.next(value);
  }
  loadData() {
    const skipCount = this.page * this.limit;
    const maxResultCount = this.limit;

    this.loaiThietBiPhanMemService.getList({
      sorting: this.sorting,
      skipCount,
      maxResultCount,
    }).subscribe((response) => {
      this.loaiThietBiPhanMem = response;
    });
  }
  onPage(event: any) {
    this.page = event.offset;
    this.loadData();
  }
  get totalPages(): number {
    return Math.ceil(this.loaiThietBiPhanMem.totalCount / this.limit) || 1;
  }

  get pages(): number[] {
    // Hiển thị tối đa 3 trang, có thể custom thêm
    const pages = [];
    const start = Math.max(0, this.page - 1);
    const end = Math.min(this.totalPages, start + 3);
    for (let i = start; i < end; i++) {
      pages.push(i);
    }
    return pages;
  }
  goToPage(page: number) {
    if (page < 0 || page >= this.totalPages) return;
    this.page = page;
    this.loadData();
  }
  onLimitChange(limit: number) {
    this.limit = limit;
    this.page = 0;
    this.loadData();
  }
  createLoaiThietBiPhanMem() {
    this.selectedLoaiThietBiPhanMem = {} as LoaiThietBiDichVuPhanMemDto; // reset the selected loaiThietBiPhanMem
    this.buildForm();
    this.isModalOpen = true;
  }
  editLoaiThietBiPhanMem(id: number) {
    this.loaiThietBiPhanMemService.get(id).subscribe((loaiThietBiPhanMem) => {
      this.selectedLoaiThietBiPhanMem = loaiThietBiPhanMem;
      this.buildForm();
      this.isModalOpen = true;
    });
  }
  delete(id: number) {
    this.confirmation.warn('::Bạn có chắc muốn xóa không?', '::Cảnh báo').subscribe((status) => {
      if (status === Confirmation.Status.confirm) {
        this.loaiThietBiPhanMemService.delete(id).subscribe(() => {
          this.list.get();
        });
      }
    });
  }
  buildForm() {
    this.form = this.fb.group({
      id: [this.selectedLoaiThietBiPhanMem.id],
      tenLoaiThietBiDichVuPhanMem: [this.selectedLoaiThietBiPhanMem.tenLoaiThietBiDichVuPhanMem, [Validators.required]],
      maLoaiThietBiDichVuPhanMem: [this.selectedLoaiThietBiPhanMem.maLoaiThietBiDichVuPhanMem],
      trangThai: [this.selectedLoaiThietBiPhanMem.trangThai, [Validators.required]],
      ghiChu: [this.selectedLoaiThietBiPhanMem.ghiChu],
    });
  }

  save() {
    if (this.form.invalid) {
      return;
    }
    const input = this.form.value;
    if (input.id) {
      this.loaiThietBiPhanMemService.update(input.id, input).subscribe(() => {
        this.isModalOpen = false;
        this.list.get();
      });
    } else {
      this.loaiThietBiPhanMemService.create(input).subscribe(() => {
        this.isModalOpen = false;
        this.list.get();
      });
    }
  }
}
