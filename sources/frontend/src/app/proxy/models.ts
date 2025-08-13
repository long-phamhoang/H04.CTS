import type { AuditedEntityDto } from '@abp/ng.core';
import type { TrangThai } from './utilities/trang-thai.enum';

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

export interface CreateUpdateLucLuongDto {
  tenLucLuong?: string;
  maLucLuong?: string;
  trangThai?: TrangThai;
  ghiChu?: string;
}

export interface LucLuongDto extends AuditedEntityDto<number> {
  tenLucLuong?: string;
  maLucLuong?: string;
  trangThai?: TrangThai;
  ghiChu?: string;
}

export interface DieuKienCapCTSTheoLL_CreateUpdateDto {
  tenDieuKien?: string;
  maDieuKien?: string;
  trangThai?: TrangThai;
  ghiChu?: string;
  lucLuongId?: number;
}

export interface DieuKienCapCTSTheoLLDto extends AuditedEntityDto<number> {
  tenDieuKien?: string;
  maDieuKien?: string;
  trangThai?: TrangThai;
  ghiChu?: string;
  lucLuongId?: number;
}