import type { AuditedEntityDto } from '@abp/ng.core';
import type { TrangThai } from './enums';

export interface CreateUpdateToChucDto {
  toChucCapTrenId?: number;
  tenToChuc: string;
  maToChuc: string;
  maSoThue?: string;
  diaChiThuCongVu?: string;
  dienThoai?: string;
  maQuanHeNganSach?: string;
  capCoQuanId?: number;
  soNha?: string;
  tinhThanhPhoId?: number;
  xaPhuongId?: number;
  coQuanPhuTrach?: string;
  trangThai?: TrangThai;
  ghiChu?: string;
}

export interface ToChucDto extends AuditedEntityDto<number> {
  toChucCapTrenId?: number;
  tenToChuc?: string;
  maToChuc?: string;
  maSoThue?: string;
  diaChiThuCongVu?: string;
  dienThoai?: string;
  maQuanHeNganSach?: string;
  capCoQuanId?: number;
  soNha?: string;
  tinhThanhPhoId?: number;
  xaPhuongId?: number;
  coQuanPhuTrach?: string;
  trangThai?: TrangThai;
  ghiChu?: string;
}

export interface CreateUpdateLoaiThietBiDichVuPhanMemDto {
  tenLoaiThietBiDichVuPhanMem: string;
  maLoaiThietBiDichVuPhanMem: string;
  trangThai?: TrangThai;
  ghiChu?: string;
}

export interface LoaiThietBiDichVuPhanMemDto extends AuditedEntityDto<number> {
  tenLoaiThietBiDichVuPhanMem: string;
  maLoaiThietBiDichVuPhanMem: string;
  trangThai?: TrangThai;
  ghiChu?: string;
}
