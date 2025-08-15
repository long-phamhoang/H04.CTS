import { RestService, Rest } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { CreateUpdateToChucDto, ToChucDto } from '../models';

@Injectable({
  providedIn: 'root',
})
export class ToChucService {
  apiName = 'Default';

  create = (input: CreateUpdateToChucDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ToChucDto>({
      method: 'POST',
      url: '/api/app/to-chuc',
      body: input,
    },
    { apiName: this.apiName,...config });

  delete = (id: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/to-chuc/${id}`,
    },
    { apiName: this.apiName,...config });

  get = (id: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ToChucDto>({
      method: 'GET',
      url: `/api/app/to-chuc/${id}`,
    },
    { apiName: this.apiName,...config });

  getList = (input: PagedAndSortedResultRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<ToChucDto>>({
      method: 'GET',
      url: '/api/app/to-chuc',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });

  softDelete = (id: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: `/api/app/to-chuc/${id}/soft-delete`,
    },
    { apiName: this.apiName,...config });

  update = (id: number, input: CreateUpdateToChucDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ToChucDto>({
      method: 'PUT',
      url: `/api/app/to-chuc/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  getToChucDropdown = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, ToChucDto[]>({
      method: 'GET',
      url: '/api/app/to-chuc/to-chuc-for-drop-down',
    },
    { apiName: this.apiName, ...config });
  constructor(private restService: RestService) {}
}
