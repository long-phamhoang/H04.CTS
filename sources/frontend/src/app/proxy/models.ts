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

//Models for ChucVu
export interface CreateUpdateChucVuDto {
  tenChucVu: string;
  maChucVu: string;
  moTa?: string;
  trangThai?: TrangThai;
  ghiChu?: string;
}

export interface ChucVuDto extends AuditedEntityDto<number> {
  tenChucVu?: string;
  maChucVu?: string;
  moTa?: string;
  trangThai?: TrangThai;
  ghiChu?: string;
}

export interface GetAllChucVusInput {
  sorting?: string;
  skipCount?: number;
  maxResultCount?: number;
  filterInput: string;
}

//Model ThueBaoCaNhan
export interface CreateUpdateThueBaoCaNhanDto {
  hoTen: string;
  ngaySinh: string; // ISO Date string, required in C#
  soDinhDanhCaNhan: string;
  noiCap: string;
  ngayCap: string; // ISO Date string
  toChucId: number;
  chucVuId: number;
  diaChiThuDienTuCongVu: string;
  tinhThanhPho?: number; // Optional
  phuongXa?: number; // Optional
}
export interface ThueBaoCaNhanDto extends AuditedEntityDto<number> {
  hoTen: string;
  ngaySinh: string;
  soDinhDanhCaNhan: string;
  noiCap: string;
  ngayCap: string;
  toChucId: number;
  chucVuId: number;
  diaChiThuDienTuCongVu: string;
  tinhThanhPho?: number;
  phuongXa?: number;
  toChuc?: ToChucDto;
  chucVu?: ChucVuDto;
}
export interface GetAllThueBaoCaNhanInput {
  sorting?: string;
  skipCount?: number;
  maxResultCount?: number;
  filterInput: string;
}
