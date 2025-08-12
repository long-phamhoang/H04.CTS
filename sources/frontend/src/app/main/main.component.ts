import { Component } from '@angular/core';
import { MenuItem } from 'primeng/api';

@Component({
  standalone: false,
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.less'],
})
export class MainComponent {
  navMenu: MenuItem[] = [
    {
      label: 'Bảng thống kê', url: '/dashboard',
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
        { label: 'Tỉnh, thành phố', url: '/main/danh-muc/tinh-thanh-pho', },
        { label: 'Xã, phường', url: '/main/danh-muc/xa-phuong', },
        { label: 'Cấp cơ quan', url: '/main/danh-muc/cap-co-quan', },
        { label: 'Tổ chức', url: '/main/danh-muc/to-chuc', },
        { label: 'Loại thiết bị, dịch vụ', url: '/main/danh-muc/loai-thiet-bi-dich-vu', },
        { label: 'Thiết bị dịch vụ, phần mềm', url: '/main/danh-muc/thiet-bi-dich-vu-phan-mem', },
        { label: 'Chức vụ', url: '/main/danh-muc/chuc-vu', },
        { label: 'Thuê bao cá nhân', url: '/main/danh-muc/thue-bao-ca-nhan', },
        { label: 'Nơi cấp CCCD', url: '/main/danh-muc/noi-cap-cccd', },
        { label: 'Người tiếp nhận', url: '/main/danh-muc/nguoi-tiep-nhan', },
        { label: 'Loại hồ sơ', url: '/main/danh-muc/loai-ho-so', },
        { label: 'Loại chứng thư số', url: '/main/danh-muc/loai-chung-thu-so', },
        { label: 'Trích yếu', url: '/main/danh-muc/trich-yeu', },
        { label: 'Chứng thư số và thiết bị', url: '/main/danh-muc/chung-thu-so-va-thiet-bi', },
      ]
    },
    {
      label: 'Quản trị hệ thống', items: [
        { label: 'Người dùng', url: '/accounts', },
        { label: 'Vai trò', url: '/roles', },
        { label: 'Thiết lập', url: '/settings', },
      ]
    },
  ];

  userMenu: MenuItem[] = [
    { label: 'Thông tin cá nhân', icon: 'pi pi-user', routerLink: '/profile' },
    { label: 'Đổi mật khẩu', icon: 'pi pi-key', routerLink: '/change-password' },
    { label: 'Đăng xuất', icon: 'pi pi-sign-out', command: () => this.logout() }
  ];

  constructor() { }

  logout() {

  }
}
