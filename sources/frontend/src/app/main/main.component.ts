import { AuthService, ConfigStateService, LocalizationService, SessionStateService } from '@abp/ng.core';
import { DatePipe } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { MenuItem, SelectItem } from 'primeng/api';
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
      label: '::Menu:DanhMucs', items: [
        { label: '::Menu:TinhThanhPho', routerLink: '/main/danh-muc/tinh-thanh-pho', }, // DCIM Lê Thế Kỳ
        { label: '::Menu:XaPhuong', routerLink: '/main/danh-muc/xa-phuong', }, // DCIM Lê Thế Kỳ
        { label: '::Menu:CapCoQuan', routerLink: '/main/danh-muc/cap-co-quan', }, // TA Nguyễn Quang Phước
        { label: '::Menu:ToChuc', routerLink: '/main/danh-muc/to-chuc', },
        { label: '::Menu:LoaiThietBiDichVuPhanMem', routerLink: '/main/danh-muc/loai-thiet-bi-dich-vu-phan-mem', }, // TA Dương Xuân Lộc
        { label: '::Menu:ThietBiDichVuPhanMem', routerLink: '/main/danh-muc/thiet-bi-dich-vu-phan-mem', }, // TA Dương Xuân Lộc
        { label: '::Menu:ChucVu', routerLink: '/main/danh-muc/chuc-vu', }, // CA Phạm Ngọc Thuỷ
        { label: '::Menu:ThueBaoCaNhan', routerLink: '/main/danh-muc/thue-bao-ca-nhan', }, // CA Phạm Ngọc Thuỷ
        { label: '::Menu:NoiCapCCCD', routerLink: '/main/danh-muc/noi-cap-cccd', }, // DCIM Trịnh Đức Thành
        { label: '::Menu:NguoiTiepNhan', routerLink: '/main/danh-muc/nguoi-tiep-nhan', }, // DCIM Trịnh Đức Thành
        { label: '::Menu:LoaiHoSo', routerLink: '/main/danh-muc/loai-ho-so', }, // TA Tạ Đức Hoàn
        { label: '::Menu:LoaiChungThuSo', routerLink: '/main/danh-muc/loai-chung-thu-so', }, // TA Tạ Đức Hoàn
        { label: '::Menu:MangHeThongCapCts', routerLink: '/main/danh-muc/mang-he-thong-cap-cts', }, // TA Trần Đức Minh
        { label: '::Menu:TrichYeu', routerLink: '/main/danh-muc/trich-yeu', }, // TA Trần Đức Minh,
        { label: '::Menu:LucLuong', routerLink: '/main/danh-muc/trich-yeu', }, // CA Nguyễn Công Tâm
        { label: '::Menu:DieuKienCapCTSTheoLL', routerLink: '/main/danh-muc/dieu-kien-cap-cts-theo-luc-luong', }, // CA Nguyễn Công Tâm
        { label: '::Menu:LoaiThietBi', routerLink: '/main/danh-muc/loai-thiet-bi', },
        { label: '::Menu:ChungThuSoVaThietBi', routerLink: '/main/danh-muc/chung-thu-so-va-thiet-bi', }, // TA Nguyễn Quang Phước
      ]
    },
    {
      label: '::Menu:SystemAdministration', items: [
        { label: '::Menu:NguoiDung', routerLink: '/identity/users', },
        { label: '::Menu:VaiTro', routerLink: '/identity/roles', },
        { label: '::Menu:CaiDat', routerLink: '/setting-management', },
      ]
    },
  ];

  currentTime: string;
  currentTimeSubscription: Subscription | undefined;

  currentLang: string;
  langOptions: SelectItem[] = [];

  currentUser: any;
  userOptions: MenuItem[] = [
    { label: '::UserMenu:Profile', icon: 'pi pi-user', routerLink: '/account/manage' },
    { label: '::UserMenu:ChangePassword', icon: 'pi pi-key', routerLink: '/account/manage' },
    { label: '::UserMenu:Logout', icon: 'pi pi-sign-out', command: () => this.logout() }
  ];
  //#endregion

  //#region Constructor and Lifecycle
  constructor(
    private datePipe: DatePipe,
    private authService: AuthService,
    private configState: ConfigStateService,
    private localizationService: LocalizationService,
    private sessionStateService: SessionStateService,
  ) { }

  ngOnInit(): void {
    this.navMenu.forEach((menu) => {
      menu.label = this.localizationService.instant(menu.label);
      menu.items?.forEach((submenu) => {
        submenu.label = this.localizationService.instant(submenu.label);
      });
    });

    this.currentTimeSubscription = interval(1000).subscribe(() => {
      const now = new Date();
      this.currentTime = this.datePipe.transform(now, 'EEEE, dd/MM/yyyy - HH:mm');
    });

    this.currentLang = this.sessionStateService.getLanguage();
    this.langOptions = this.configState.getDeep('localization.languages').map((lang) => ({
      label: lang.displayName, value: lang.cultureName, icon: `assets/images/flag-of-${lang.uiCultureName}.png`
    }));

    this.currentUser = this.configState.getOne('currentUser');
    this.userOptions.forEach((option) => option.label = this.localizationService.instant(option.label));
    this.userOptions.unshift({ label: this.currentUser.email, disabled: true });
  }

  ngOnDestroy(): void {
    if (this.currentTimeSubscription) {
      this.currentTimeSubscription.unsubscribe();
    }
  }
  //#endregion

  //#region Custom Methods
  getUserDisplayName() {
    return this.currentUser.surname && this.currentUser.name
      ? `${this.currentUser.surname} ${this.currentUser.surname}`
      : this.currentUser.userName
  }
  //#endregion

  //#region Main Methods
  changeLang(lang: string) {
    this.sessionStateService.setLanguage(lang);
  }

  logout() {
    this.authService.logout().subscribe();
  }
  //#endregion
}
