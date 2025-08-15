import { RestService, Rest } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { CreateUpdateLoaiCTS, LoaiCTSDto } from '../models';

@Injectable({
  providedIn: 'root',
})
export class LoaiCTSService {
  apiName = 'Default';
  

  create = (input: CreateUpdateLoaiCTS, config?: Partial<Rest.Config>) =>
    this.restService.request<any, LoaiCTSDto>({
      method: 'POST',
      url: '/api/app/loai-cTS',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/loai-cTS/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, LoaiCTSDto>({
      method: 'GET',
      url: `/api/app/loai-cTS/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: PagedAndSortedResultRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<LoaiCTSDto>>({
      method: 'GET',
      url: '/api/app/loai-cTS',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });

  getFilterList = (input: any, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<LoaiCTSDto>>({
      method: 'GET',
      url: '/api/app/loai-cTS/filter-data',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount, keyword: input.keyword },
    },
    { apiName: this.apiName,...config });
  
  
  isExistsMaLoaiCTS = (maLoaiCTS: string, id?: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, LoaiCTSDto>({
      method: 'POST',
      url: `/api/app/loai-cTS/${id ?? 0}/is-exists-ma-loai-cTS?maLoaiCTS=${maLoaiCTS}`,
    },
    { apiName: this.apiName,...config });

  softDelete = (id: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/loai-cts/soft-delete/${id}`,
    },
    { apiName: this.apiName,...config });
  

  update = (id: number, input: CreateUpdateLoaiCTS, config?: Partial<Rest.Config>) =>
    this.restService.request<any, LoaiCTSDto>({
      method: 'PUT',
      url: `/api/app/loai-cTS/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
