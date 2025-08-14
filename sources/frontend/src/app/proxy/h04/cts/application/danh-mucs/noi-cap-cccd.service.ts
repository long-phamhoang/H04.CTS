import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { CreateUpdateNoiCapCCCDDto, GetNoiCapCCCDListDto, NoiCapCCCDDto } from '../../dtos/danh-mucs/models';

@Injectable({
  providedIn: 'root',
})
export class NoiCapCCCDService {
  apiName = 'Default';
  

  create = (input: CreateUpdateNoiCapCCCDDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, NoiCapCCCDDto>({
      method: 'POST',
      url: '/api/app/noi-cap-cCCD',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/noi-cap-cCCD/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, NoiCapCCCDDto>({
      method: 'GET',
      url: `/api/app/noi-cap-cCCD/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetNoiCapCCCDListDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<NoiCapCCCDDto>>({
      method: 'GET',
      url: '/api/app/noi-cap-cCCD',
      params: { keyword: input.keyword, name: input.name, code: input.code, abbreviation: input.abbreviation, address: input.address, province: input.province, note: input.note, isActive: input.isActive, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: number, input: CreateUpdateNoiCapCCCDDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, NoiCapCCCDDto>({
      method: 'PUT',
      url: `/api/app/noi-cap-cCCD/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
