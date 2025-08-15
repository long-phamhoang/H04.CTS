import { PagedAndSortedResultRequestDto } from "@abp/ng.core";
import { Injectable } from "@angular/core";
import { RestService, Rest } from '@abp/ng.core';
import { CreateUpdateTinhThanhPhoDto, TinhThanhPhoDto } from "../models";
import { PagedResultDto } from "@abp/ng.core";

@Injectable({
    providedIn: 'root',
  })
export class TinhThanhPhoService {
    apiName = 'Default';

    create = (input: CreateUpdateTinhThanhPhoDto, config?: Partial<Rest.Config>) =>
        this.restService.request<any, TinhThanhPhoDto>({
          method: 'POST',
          url: '/api/app/tinh-thanh-pho',
          body: input,
        },
        { apiName: this.apiName,...config });
    
      delete = (id: string, config?: Partial<Rest.Config>) =>
        this.restService.request<any, void>({
          method: 'DELETE',
          url: `/api/app/tinh-thanh-pho/${id}`,
        },
        { apiName: this.apiName,...config });
    
      get = (id: string, config?: Partial<Rest.Config>) =>
        this.restService.request<any, TinhThanhPhoDto>({
          method: 'GET',
          url: `/api/app/tinh-thanh-pho/${id}`,
        },
        { apiName: this.apiName,...config });
    
      getList = (input: PagedAndSortedResultRequestDto, searchValue?: string, config?: Partial<Rest.Config>) =>
        this.restService.request<any, PagedResultDto<TinhThanhPhoDto>>({
          method: 'GET',
          url: '/api/app/tinh-thanh-pho',
          params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount, tenTinhThanhPho: searchValue || null },
        },
        { apiName: this.apiName,...config });
    
      softDelete = (id: number, config?: Partial<Rest.Config>) =>
        this.restService.request<any, void>({
          method: 'POST',
          url: `/api/app/tinh-thanh-pho/${id}/soft-delete`,
        },
        { apiName: this.apiName,...config });
    
      update = (id: string, input: CreateUpdateTinhThanhPhoDto, config?: Partial<Rest.Config>) =>
        this.restService.request<any, TinhThanhPhoDto>({
          method: 'PUT',
          url: `/api/app/tinh-thanh-pho/${id}`,
          body: input,
        },
        { apiName: this.apiName,...config });
    
      constructor(private restService: RestService) {}
}