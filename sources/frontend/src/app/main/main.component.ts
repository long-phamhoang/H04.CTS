import { AuthService } from '@abp/ng.core';
import { DatePipe } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { interval, Subscription } from 'rxjs';

@Component({
  standalone: false,
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.less'],
  providers: [DatePipe]
})
export class MainComponent implements OnInit, OnDestroy {
  //#region Variables
  navMenu: MenuItem[] = [
    {
      label: 'Bảng thống kê', routerLink: '/dashboard',
    },
    {
      label: 'Văn thư', items: [

      ]
    },
    {
      label: 'Hồ sơ văn bản đến', items: [

      ]
    },
    {
      label: 'Danh mục', items: [
        { label: 'Tỉnh, thành phố', routerLink: '/main/danh-muc/tinh-thanh-pho', }, // DCIM Lê Thế Kỳ
        { label: 'Xã, phường', routerLink: '/main/danh-muc/xa-phuong', }, // DCIM Lê Thế Kỳ
        { label: 'Cấp cơ quan', routerLink: '/main/danh-muc/cap-co-quan', }, // ToaAn Nguyễn Quang Phước
        { label: 'Tổ chức', routerLink: '/main/danh-muc/to-chuc', },
        { label: 'Loại thiết bị, dịch vụ', routerLink: '/main/danh-muc/loai-thiet-bi-dich-vu', }, // ToaAn Dương Xuân Lộc
        { label: 'Thiết bị dịch vụ, phần mềm', routerLink: '/main/danh-muc/thiet-bi-dich-vu-phan-mem', }, // ToaAn Dương Xuân Lộc
        { label: 'Chức vụ', routerLink: '/main/danh-muc/chuc-vu', }, // CheAp Phạm Ngọc Thuỷ
        { label: 'Thuê bao cá nhân', routerLink: '/main/danh-muc/thue-bao-ca-nhan', }, // CheAp Phạm Ngọc Thuỷ
        { label: 'Nơi cấp CCCD', routerLink: '/main/danh-muc/noi-cap-cccd', }, // DCIM Trịnh Đức Thành
        { label: 'Người tiếp nhận', routerLink: '/main/danh-muc/nguoi-tiep-nhan', }, // DCIM Trịnh Đức Thành
        { label: 'Loại hồ sơ', routerLink: '/main/danh-muc/loai-ho-so', }, // ToaAn Tạ Đức Hoàn
        { label: 'Loại chứng thư số', routerLink: '/main/danh-muc/loai-chung-thu-so', }, // ToaAn Tạ Đức Hoàn
        { label: 'Mạng, hệ thống cấp CTS', routerLink: '/main/danh-muc/mang-he-thong-cap-cts', }, // ToaAn Trần Đức Minh
        { label: 'Trích yếu', routerLink: '/main/danh-muc/trich-yeu', }, // ToaAn Trần Đức Minh
      ]
    },
    {
      label: 'Quản trị hệ thống', items: [
        { label: 'Người dùng', routerLink: '/identity/users', },
        { label: 'Vai trò', routerLink: '/identity/roles', },
        { label: 'Cài đặt', routerLink: '/setting-management', },
      ]
    },
  ];

  userMenu: MenuItem[] = [
    { label: 'Thông tin cá nhân', icon: 'pi pi-user', routerLink: '/profile' },
    { label: 'Đổi mật khẩu', icon: 'pi pi-key', routerLink: '/change-password' },
    { label: 'Đăng xuất', icon: 'pi pi-sign-out', command: () => this.logout() }
  ];

  currentTime: string;
  currentTimeSubscription: Subscription | undefined;
  //#endregion

  //#region Constructor and Lifecycle
  constructor(
    private datePipe: DatePipe,
    private authService: AuthService,
  ) { }

  ngOnInit(): void {
    this.currentTimeSubscription = interval(1000).subscribe(() => {
      const now = new Date();
      this.currentTime = this.datePipe.transform(now, 'EEEE, dd/MM/yyyy - HH:mm');
    });
  }

  ngOnDestroy(): void {
    if (this.currentTimeSubscription) {
      this.currentTimeSubscription.unsubscribe();
    }
  }
  //#endregion

  //#region Main methods
  logout() {
    this.authService.logout().subscribe();
  }
  //#endregion
}
