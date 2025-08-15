import type { AuditedEntityDto } from '@abp/ng.core';
import type { TrangThai } from './enums';

export interface CreateUpdateLoaiCTS {
  maLoaiCTS?: string;
  tenLoaiCTS?: string;
  trangThai?: TrangThai;
  ghiChu?: string;
}

export interface LoaiCTSDto extends AuditedEntityDto<number> {
  maLoaiCTS?: string;
  tenLoaiCTS?: string;
  trangThai?: TrangThai;
  ghiChu?: string;
}

export interface CreateUpdateLoaiHoSoDto {
  maLoaiHoSo: string;
  tenLoaiHoSo: string;
  trangThai?: TrangThai;
  ghiChu?: string;
}

export interface LoaiHoSoDto extends AuditedEntityDto<number> {
  maLoaiHoSo?: string;
  tenLoaiHoSo?: string;
  trangThai?: TrangThai;
  ghiChu?: string;
}

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
