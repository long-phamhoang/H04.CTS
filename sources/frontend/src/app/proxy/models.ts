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

export interface CreateUpdateTinhThanhPhoDto {
  tenTinhThanhPho: string;
  maTinhThanhPho: string;
  trangThai?: TrangThai;
  ghiChu?: string;
}

export interface TinhThanhPhoDto extends AuditedEntityDto<string> {
  tenTinhThanhPho?: string;
  maTinhThanhPho?: string;
  trangThai?: TrangThai;
  ghiChu?: string;
}

export interface CreateUpdateXaPhuongDto {
  tenXaPhuong: string;
  maXaPhuong: string;
  trangThai?: TrangThai;
  ghiChu?: string;
}

export interface XaPhuongDto extends AuditedEntityDto<string> { 
  tinhThanhPhoId?: string;
  tenXaPhuong?: string;
  maXaPhuong?: string;
  trangThai?: TrangThai;
  ghiChu?: string;
}