import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from "@angular/core";
import { CreateUpdateTrichYeuDto, GetAllTrichYeuInput, TrichYeuDto } from '../dtos';

@Injectable({
  providedIn: 'root',
})
export class TrichYeuService {
  apiName = 'Default';

  constructor(private restService: RestService) { }

  create = (input: CreateUpdateTrichYeuDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TrichYeuDto>({
      method: 'POST',
      url: '/api/app/trich-yeu',
      body: input,
    },
      { apiName: this.apiName, ...config });

  delete = (id: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/trich-yeu/${id}/soft-delete`,
    },
      { apiName: this.apiName, ...config });

  get = (id: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TrichYeuDto>({
      method: 'GET',
      url: `/api/app/trich-yeu/${id}`,
    },
      { apiName: this.apiName, ...config });

  getList = (input: GetAllTrichYeuInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<TrichYeuDto>>({
      method: 'GET',
      url: '/api/app/trich-yeu',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount, filterInput: input.filterInput },
    },
      { apiName: this.apiName, ...config });

  update = (id: number, input: CreateUpdateTrichYeuDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TrichYeuDto>({
      method: 'PUT',
      url: `/api/app/trich-yeu/${id}`,
      body: input,
    },
      { apiName: this.apiName, ...config });

  checkMaDuplicate = (maTrichYeu: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, boolean>({
      method: 'PUT',
      url: `/api/app/trich-yeu/check-ma/${maTrichYeu}`,
    },
      { apiName: this.apiName, ...config });
}