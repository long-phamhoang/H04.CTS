import { RestService, Rest } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { CreateUpdateLoaiHoSoDto, LoaiHoSoDto } from '../models';

@Injectable({
  providedIn: 'root',
})
export class LoaiHoSoService {
  apiName = 'Default';
  

  create = (input: CreateUpdateLoaiHoSoDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, LoaiHoSoDto>({
      method: 'POST',
      url: '/api/app/loai-ho-so',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/loai-ho-so/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, LoaiHoSoDto>({
      method: 'GET',
      url: `/api/app/loai-ho-so/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: PagedAndSortedResultRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<LoaiHoSoDto>>({
      method: 'GET',
      url: '/api/app/loai-ho-so',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  
  getFilterList = (input: any, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<LoaiHoSoDto>>({
      method: 'GET',
      url: '/api/app/loai-ho-so/filter-data',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount, keyword: input.keyword },
    },
    { apiName: this.apiName,...config });
  
  isExistsMaLoaiHoSo = (maLoaiHoSo: string, id?: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, LoaiHoSoDto>({
      method: 'POST',
      url: `/api/app/loai-ho-so/${id ?? 0}/is-exists-ma-loai-ho-so?maLoaiHoSo=${maLoaiHoSo}`,
    },
    { apiName: this.apiName,...config });

  softDelete = (id: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/loai-ho-so/soft-delete/${id}`,
    },
    { apiName: this.apiName,...config });
  

  update = (id: number, input: CreateUpdateLoaiHoSoDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, LoaiHoSoDto>({
      method: 'PUT',
      url: `/api/app/loai-ho-so/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
