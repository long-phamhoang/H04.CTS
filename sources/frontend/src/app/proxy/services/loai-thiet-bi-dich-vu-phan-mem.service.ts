import { RestService, Rest } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

import type { CreateUpdateLoaiThietBiDichVuPhanMemDto, LoaiThietBiDichVuPhanMemDto } from '../models';

@Injectable({
    providedIn: 'root',
})
export class LoaiThietBiDichVuPhanMemService {
  apiName = 'Default';

  create = (input: CreateUpdateLoaiThietBiDichVuPhanMemDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, LoaiThietBiDichVuPhanMemDto>({
      method: 'POST',
      url: '/api/app/loai-thiet-bi-dich-vu-phan-mem',
      body: input,
    },
    { apiName: this.apiName, ...config });

  delete = (id: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/loai-thiet-bi-dich-vu-phan-mem/${id}`,
    },
    { apiName: this.apiName, ...config });

  get = (id: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, LoaiThietBiDichVuPhanMemDto>({
      method: 'GET',
      url: `/api/app/loai-thiet-bi-dich-vu-phan-mem/${id}`,
    },
    { apiName: this.apiName, ...config });

  getList = (input: PagedAndSortedResultRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<LoaiThietBiDichVuPhanMemDto>>({
      method: 'GET',
      url: '/api/app/loai-thiet-bi-dich-vu-phan-mem',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName, ...config });

  update = (id: number, input: CreateUpdateLoaiThietBiDichVuPhanMemDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, LoaiThietBiDichVuPhanMemDto>({
      method: 'PUT',
      url: `/api/app/loai-thiet-bi-dich-vu-phan-mem/${id}`,
      body: input,
    },
    { apiName: this.apiName, ...config });

  constructor(private restService: RestService) {}
}