import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from "@angular/core";
import { CreateUpdateMangCTSDto, GetAllTrichYeuInput, MangCTSDto } from '../dtos';

@Injectable({
    providedIn: 'root',
})
export class MangHeThongCapCTSService {
    apiName = 'Default';

    constructor(private restService: RestService) { }

    create = (input: CreateUpdateMangCTSDto, config?: Partial<Rest.Config>) =>
        this.restService.request<any, MangCTSDto>({
            method: 'POST',
            url: '/api/app/mang-he-thong-cap-cts',
            body: input,
        },
            { apiName: this.apiName, ...config });

    delete = (id: number, config?: Partial<Rest.Config>) =>
        this.restService.request<any, void>({
            method: 'DELETE',
            url: `/api/app/mang-he-thong-cap-cts/${id}/soft-delete`,
        },
            { apiName: this.apiName, ...config });

    get = (id: number, config?: Partial<Rest.Config>) =>
        this.restService.request<any, MangCTSDto>({
            method: 'GET',
            url: `/api/app/mang-he-thong-cap-cts/${id}`,
        },
            { apiName: this.apiName, ...config });

    getList = (input: GetAllTrichYeuInput, config?: Partial<Rest.Config>) =>
        this.restService.request<any, PagedResultDto<MangCTSDto>>({
            method: 'GET',
            url: '/api/app/mang-he-thong-cap-cts',
            params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount, filterInput: input.filterInput },
        },
            { apiName: this.apiName, ...config });

    update = (id: number, input: CreateUpdateMangCTSDto, config?: Partial<Rest.Config>) =>
        this.restService.request<any, MangCTSDto>({
            method: 'PUT',
            url: `/api/app/mang-he-thong-cap-cts/${id}`,
            body: input,
        },
            { apiName: this.apiName, ...config });

    checkMaDuplicate = (maMangCTS: string, config?: Partial<Rest.Config>) =>
        this.restService.request<any, boolean>({
            method: 'PUT',
            url: `/api/app/mang-he-thong-cap-cts/check-ma/${maMangCTS}`,
        },
            { apiName: this.apiName, ...config });
}