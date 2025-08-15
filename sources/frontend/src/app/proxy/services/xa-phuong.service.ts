import { PagedAndSortedResultRequestDto } from "@abp/ng.core";
import { Injectable } from "@angular/core";
import { RestService, Rest } from '@abp/ng.core';
import { CreateUpdateXaPhuongDto, XaPhuongDto } from "../models";
import { PagedResultDto } from "@abp/ng.core";

@Injectable({
    providedIn: 'root',
  })
export class XaPhuongService {
    apiName = 'Default';

    create = (input: CreateUpdateXaPhuongDto, config?: Partial<Rest.Config>) =>
        this.restService.request<any, XaPhuongDto>({
          method: 'POST',
          url: '/api/app/xa-phuong/ward',
          body: input,
        },
        { apiName: this.apiName,...config });
    
      delete = (id: string, config?: Partial<Rest.Config>) =>
        this.restService.request<any, void>({
          method: 'DELETE',
          url: `/api/app/xa-phuong/${id}/ward`,
        },
        { apiName: this.apiName,...config });
    
      get = (id: string, config?: Partial<Rest.Config>) =>
        this.restService.request<any, XaPhuongDto>({
          method: 'GET',
          url: `/api/app/xa-phuong/${id}/ward-by-id`,
        },
        { apiName: this.apiName,...config });
    
      getList = (input: PagedAndSortedResultRequestDto, searchValue?: string, tinhThanhPhoId?: string, config?: Partial<Rest.Config>) =>
        this.restService.request<any, PagedResultDto<XaPhuongDto>>({
          method: 'GET',
          url: '/api/app/xa-phuong',
          params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount, tenXaPhuong: searchValue || null ,tinhThanhPhoId: tinhThanhPhoId || null,},
        },
        { apiName: this.apiName,...config });
    
      softDelete = (id: string, config?: Partial<Rest.Config>) =>
        this.restService.request<any, void>({
          method: 'POST',
          url: `/api/app/xa-phuong/${id}/ward`,
        },
        { apiName: this.apiName,...config });
    
      update = (id: string, input: CreateUpdateXaPhuongDto, config?: Partial<Rest.Config>) =>
        this.restService.request<any, XaPhuongDto>({
          method: 'PUT',
          url: `/api/app/xa-phuong/${id}/ward`,
          body: input,
        },
        { apiName: this.apiName,...config });
    
      constructor(private restService: RestService) {}
}