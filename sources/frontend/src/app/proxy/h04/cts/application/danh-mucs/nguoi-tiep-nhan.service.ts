import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { CreateUpdateNguoiTiepNhanDto, GetNguoiTiepNhanListDto, NguoiTiepNhanDto } from '../../dtos/danh-mucs/models';

@Injectable({
  providedIn: 'root',
})
export class NguoiTiepNhanService {
  apiName = 'Default';
  

  create = (input: CreateUpdateNguoiTiepNhanDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, NguoiTiepNhanDto>({
      method: 'POST',
      url: '/api/app/nguoi-tiep-nhan',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/nguoi-tiep-nhan/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, NguoiTiepNhanDto>({
      method: 'GET',
      url: `/api/app/nguoi-tiep-nhan/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetNguoiTiepNhanListDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<NguoiTiepNhanDto>>({
      method: 'GET',
      url: '/api/app/nguoi-tiep-nhan',
      params: { keyword: input.keyword, organizationId: input.organizationId, fullName: input.fullName, cccd: input.cccd, dateOfIssue: input.dateOfIssue, noiCapCCCDId: input.noiCapCCCDId, position: input.position, phone: input.phone, email: input.email, submissionAddress: input.submissionAddress, province: input.province, ward: input.ward, isDefault: input.isDefault, isDeleted: input.isDeleted, deletedBy: input.deletedBy, deletedAt: input.deletedAt, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: number, input: CreateUpdateNguoiTiepNhanDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, NguoiTiepNhanDto>({
      method: 'PUT',
      url: `/api/app/nguoi-tiep-nhan/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
