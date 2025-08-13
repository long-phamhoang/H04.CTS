import { AuditedEntityDto, PagedAndSortedResultRequestDto } from "@abp/ng.core";
import { TrangThai } from "@app/proxy/enums";

export interface CreateUpdateMangCTSDto {
  maMangCTS: string;
  tenMangCTS: string;
  trangThai: TrangThai;
  ghiChu?: string;
}

export interface MangCTSDto extends AuditedEntityDto<number> {
  maMangCTS: string;
  tenMangCTS: string;
  trangThai: TrangThai;
  ghiChu?: string;
}

export interface GetAllMangCTSInput extends PagedAndSortedResultRequestDto {
  filterInput?: string;
}
