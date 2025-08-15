import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type {
  ThueBaoCaNhanDto,
  CreateUpdateThueBaoCaNhanDto,
  GetAllThueBaoCaNhanInput
} from '../models';

@Injectable({
  providedIn: 'root',
})
export class ThueBaoCaNhanService {
  apiName = 'Default';

  constructor(private restService: RestService) {}

  create = (
    input: CreateUpdateThueBaoCaNhanDto,
    config?: Partial<Rest.Config>
  ) =>
    this.restService.request<any, ThueBaoCaNhanDto>(
      {
        method: 'POST',
        url: '/api/app/thue-bao-ca-nhan',
        body: input,
      },
      { apiName: this.apiName, ...config }
    );

  update = (
    id: number,
    input: CreateUpdateThueBaoCaNhanDto,
    config?: Partial<Rest.Config>
  ) =>
    this.restService.request<any, ThueBaoCaNhanDto>(
      {
        method: 'PUT',
        url: `/api/app/thue-bao-ca-nhan/${id}`,
        body: input,
      },
      { apiName: this.apiName, ...config }
    );

  delete = (id: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>(
      {
        method: 'DELETE',
        url: `/api/app/thue-bao-ca-nhan/${id}`,
      },
      { apiName: this.apiName, ...config }
    );

  get = (id: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ThueBaoCaNhanDto>(
      {
        method: 'GET',
        url: `/api/app/thue-bao-ca-nhan/${id}`,
      },
      { apiName: this.apiName, ...config }
    );

  getList = (
    input: GetAllThueBaoCaNhanInput,
    config?: Partial<Rest.Config>
  ) =>
    this.restService.request<any, PagedResultDto<ThueBaoCaNhanDto>>(
      {
        method: 'GET',
        url: '/api/app/thue-bao-ca-nhan',
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
        url: `/api/app/thue-bao-ca-nhan/${id}/soft-delete`,
      },
      { apiName: this.apiName, ...config }
    );
}
