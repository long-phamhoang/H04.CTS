import { AuditedEntityDto, PagedAndSortedResultRequestDto } from "@abp/ng.core";
import { TrangThai } from "@app/proxy/enums";

export interface CreateUpdateTrichYeuDto {
  maTrichYeu: string;
  tenTrichYeu: string;
  trangThai: TrangThai;
  moTa?: string;
  ghiChu?: string;
}

export interface TrichYeuDto extends AuditedEntityDto<number>  {
  maTrichYeu: string;
  tenTrichYeu: string;
  trangThai: TrangThai;
  moTa?: string;
  ghiChu?: string;
}

export interface GetAllTrichYeuInput extends PagedAndSortedResultRequestDto {
  filterInput?: string;
}
