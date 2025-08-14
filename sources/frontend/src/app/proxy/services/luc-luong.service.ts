import { RestService, Rest } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { CreateUpdateLucLuongDto, LucLuongDto } from '../models';

@Injectable({
	providedIn: 'root',
})
export class LucLuongService {
	apiName = 'Default';

	create = (input: CreateUpdateLucLuongDto, config?: Partial<Rest.Config>) =>
		this.restService.request<any, LucLuongDto>({
			method: 'POST',
			url: '/api/app/luc-luong',
			body: input,
		},
		{ apiName: this.apiName,...config });

	delete = (id: number, config?: Partial<Rest.Config>) =>
		this.restService.request<any, void>({
			method: 'DELETE',
			url: `/api/app/luc-luong/${id}`,
		},
		{ apiName: this.apiName,...config });

	get = (id: number, config?: Partial<Rest.Config>) =>
		this.restService.request<any, LucLuongDto>({
			method: 'GET',
			url: `/api/app/luc-luong/${id}`,
		},
		{ apiName: this.apiName,...config });

		getList = (input: PagedAndSortedResultRequestDto & { filter?: string }, config?: Partial<Rest.Config>) =>
			this.restService.request<any, PagedResultDto<LucLuongDto>>({
				method: 'GET',
				url: '/api/app/luc-luong',
				params: { filter: input.filter, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
			},
			{ apiName: this.apiName,...config });

	update = (id: number, input: CreateUpdateLucLuongDto, config?: Partial<Rest.Config>) =>
		this.restService.request<any, LucLuongDto>({
			method: 'PUT',
			url: `/api/app/luc-luong/${id}`,
			body: input,
		},
		{ apiName: this.apiName,...config });

	constructor(private restService: RestService) {}
}


