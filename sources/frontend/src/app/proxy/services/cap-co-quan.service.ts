import { RestService, Rest } from '@abp/ng.core';
import type {  PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { CreateUpdateCapCoQuanDto, CapCoQuanDto, ListCapCoQuanRequestDto } from '../models';

@Injectable({
  providedIn: 'root',
})
export class CapCoQuanService {
  apiName = 'Default';

  create = (input: CreateUpdateCapCoQuanDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CapCoQuanDto>({
      method: 'POST',
      url: '/api/app/cap-co-quan',
      body: input,
    },
    { apiName: this.apiName,...config });

  delete = (id: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/cap-co-quan/${id}`,
    },
    { apiName: this.apiName,...config });

  get = (id: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CapCoQuanDto>({
      method: 'GET',
      url: `/api/app/cap-co-quan/${id}`,
    },
    { apiName: this.apiName,...config });

  getList = (input: ListCapCoQuanRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<CapCoQuanDto>>({
      method: 'GET',
      url: '/api/app/cap-co-quan',
      params: { filterString: input.filterString, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });

  softDelete = (id: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: `/api/app/cap-co-quan/${id}/soft-delete`,
    },
    { apiName: this.apiName,...config });

  update = (id: number, input: CreateUpdateCapCoQuanDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CapCoQuanDto>({
      method: 'PUT',
      url: `/api/app/cap-co-quan/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
