import { RestService, Rest } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { DieuKienCapCTSTheoLL_CreateUpdateDto, DieuKienCapCTSTheoLLDto } from '../models';

@Injectable({
  providedIn: 'root',
})
export class DieuKienCapCTSTheoLLService {
  apiName = 'Default';

  create = (input: DieuKienCapCTSTheoLL_CreateUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, DieuKienCapCTSTheoLLDto>({
      method: 'POST',
      url: '/api/app/dk-cts-luc-luong',
      body: input,
    }, { apiName: this.apiName, ...config });

  update = (id: number, input: DieuKienCapCTSTheoLL_CreateUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, DieuKienCapCTSTheoLLDto>({
      method: 'PUT',
      url: `/api/app/dk-cts-luc-luong/${id}`,
      body: input,
    }, { apiName: this.apiName, ...config });

  delete = (id: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/dk-cts-luc-luong/${id}`,
    }, { apiName: this.apiName, ...config });

  get = (id: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, DieuKienCapCTSTheoLLDto>({
      method: 'GET',
      url: `/api/app/dk-cts-luc-luong/${id}`,
    }, { apiName: this.apiName, ...config });

  getList = (input: PagedAndSortedResultRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<DieuKienCapCTSTheoLLDto>>({
      method: 'GET',
      url: '/api/app/dk-cts-luc-luong',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    }, { apiName: this.apiName, ...config });

  constructor(private restService: RestService) {}
}


