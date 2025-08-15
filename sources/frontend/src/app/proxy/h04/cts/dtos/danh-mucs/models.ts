import type { TrangThai } from '../../utilities/trang-thai.enum';
import type { AuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CreateUpdateNguoiTiepNhanDto {
  organizationId?: number;
  fullName?: string;
  cccd: string;
  dateOfIssue: string;
  noiCapCCCDId?: number;
  position?: string;
  phone?: string;
  email?: string;
  submissionAddress?: string;
  province?: string;
  ward?: string;
  isDefault: boolean;
}

export interface CreateUpdateNoiCapCCCDDto {
  name: string;
  code?: string;
  abbreviation?: string;
  address?: string;
  province?: string;
  note?: string;
  isActive: boolean;
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

export interface GetNguoiTiepNhanListDto extends PagedAndSortedResultRequestDto {
  keyword?: string;
  organizationId?: number;
  fullName?: string;
  cccd?: string;
  dateOfIssue?: string;
  noiCapCCCDId?: number;
  position?: string;
  phone?: string;
  email?: string;
  submissionAddress?: string;
  province?: string;
  ward?: string;
  isDefault?: boolean;
  isDeleted?: boolean;
  deletedBy?: string;
  deletedAt?: string;
}

export interface GetNoiCapCCCDListDto extends PagedAndSortedResultRequestDto {
  keyword?: string;
  name?: string;
  code?: string;
  abbreviation?: string;
  address?: string;
  province?: string;
  note?: string;
  isActive?: boolean;
}

export interface NguoiTiepNhanDto extends AuditedEntityDto<number> {
  organizationId?: number;
  organizationName?: string;
  fullName?: string;
  cccd?: string;
  dateOfIssue?: string;
  noiCapCCCDId: number;
  noiCapCCCDName?: string;
  position?: string;
  phone?: string;
  email?: string;
  submissionAddress?: string;
  province?: string;
  ward?: string;
  isDefault: boolean;
  isDeleted: boolean;
  deletedBy?: string;
  deletedAt?: string;
}

export interface NoiCapCCCDDto extends AuditedEntityDto<number> {
  name?: string;
  code?: string;
  abbreviation?: string;
  address?: string;
  province?: string;
  note?: string;
  isActive: boolean;
  isDeleted: boolean;
  deletedBy?: string;
  deletedAt?: string;
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
