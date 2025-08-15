import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { CreateUpdateChucVuDto, ChucVuDto, GetAllChucVusInput } from '../models';

@Injectable({
  providedIn: 'root',
})
export class ChucVuService {
  apiName = 'Default';

  create = (input: CreateUpdateChucVuDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ChucVuDto>(
      {
        method: 'POST',
        url: '/api/app/chuc-vu',
        body: input,
      },
      { apiName: this.apiName, ...config }
    );

  delete = (id: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>(
      {
        method: 'DELETE',
        url: `/api/app/chuc-vu/${id}`,
      },
      { apiName: this.apiName, ...config }
    );

  get = (id: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ChucVuDto>(
      {
        method: 'GET',
        url: `/api/app/chuc-vu/${id}`,
      },
      { apiName: this.apiName, ...config }
    );

  getList = (input: GetAllChucVusInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<ChucVuDto>>(
      {
        method: 'GET',
        url: '/api/app/chuc-vu',
        params: {
          filterInput: input.filterInput,
          sorting: input.sorting,
          skipCount: input.skipCount,
          maxResultCount: input.maxResultCount,
        },
      },
      { apiName: this.apiName, ...config }
    );

  softDelete = (id: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>(
      {
        method: 'POST',
        url: `/api/app/chuc-vu/${id}/soft-delete`,
      },
      { apiName: this.apiName, ...config }
    );

  update = (id: number, input: CreateUpdateChucVuDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ChucVuDto>(
      {
        method: 'PUT',
        url: `/api/app/chuc-vu/${id}`,
        body: input,
      },
      { apiName: this.apiName, ...config }
    );

  getChucVuDropdown = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, ChucVuDto[]>(
      {
        method: 'GET',
        url: '/api/app/chuc-vu/chuc-vu-for-drop-down',
      },
      { apiName: this.apiName, ...config }
    );
  isMaChucVuUnique = (maChucVu: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, boolean>(
      {
        method: 'POST',
        url: '/api/app/chuc-vu/is-ma-chuc-vu-unique',
        params: {
          maChucVu: maChucVu,
        },
      },
      { apiName: this.apiName, ...config }
    );

  constructor(private restService: RestService) {}
}
