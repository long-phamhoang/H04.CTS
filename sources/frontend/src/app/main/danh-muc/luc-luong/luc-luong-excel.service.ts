import { Injectable } from '@angular/core';
import { getTrangThaiLabel } from '@app/proxy';

@Injectable({ providedIn: 'root' })
export class LucLuongExcelService {
  // API export 
  readonly exportApiUrl: string = '/api/app/luc-luong/for-excel';

  buildRequestParams(currentFilter: string | undefined | null, totalCount?: number, sorting: string = 'TenLucLuong') {
    return {
      filter: currentFilter || '',
      sorting,
      maxCount: totalCount && totalCount > 0 ? totalCount : 10000,
    } as any;
  }

  mapExportResponse = (resp: any) => {
    const arr = Array.isArray(resp) ? resp : (resp?.items ?? []);
    return (arr || []).map((x: any) => ({
      ...x,
      trangThaiText: getTrangThaiLabel(x?.trangThai),
    }));
  };
}

