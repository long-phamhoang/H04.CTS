import { Injectable } from '@angular/core';
import { Observable, map, of, switchMap, takeWhile, timer, distinctUntilChanged } from 'rxjs';

export type ImportStatus = 'Pending' | 'Running' | 'Completed' | 'Failed';

export interface ImportProgress {
  batchId: string;
  status: ImportStatus;
  totalRows: number;
  processedRows: number;
  percent: number;
  message?: string | null;
}

export interface StartImportResult {
  batchId: string;
  progress$: Observable<ImportProgress>;
}

@Injectable({ providedIn: 'root' })
export class ImportJobService {

  normalize<T = any>(resp: any): T {
    return (resp && typeof resp === 'object' && 'data' in resp) ? (resp.data as T) : (resp as T);
  }

  pollProgress(
    batchId: string,
    getProgressFn: (batchId: string) => Observable<any>,
    pollMs: number = 1500
  ): Observable<ImportProgress> {
    return timer(0, pollMs).pipe(
      switchMap(() => getProgressFn(batchId).pipe(map(r => this.normalize<ImportProgress>(r)))),
      map(p => ({
        batchId: p.batchId || batchId,
        status: (p.status as any) || 'Running',
        totalRows: p.totalRows ?? 0,
        processedRows: p.processedRows ?? 0,
        percent: p.percent ?? (p.totalRows ? Math.floor((p.processedRows || 0) * 100 / p.totalRows) : 0),
        message: p.message,
      } as ImportProgress)),
      distinctUntilChanged((a, b) => a.status === b.status && a.percent === b.percent),
      // phát xong trạng thái cuối cùng rồi complete
      takeWhile(p => p.status === 'Running', true),
    );
  }


  start(
    file: File,
    uploadFn: (file: File) => Observable<any>,
    getProgressFn: (batchId: string) => Observable<any>,
    pollMs: number = 1500
  ): Observable<StartImportResult> {
    return uploadFn(file).pipe(
      map(r => this.normalize<any>(r)),
      map(r => (r?.batchId || r?.data?.batchId || r?.data?.data?.batchId || r?.id || r?.result || '')), 
      switchMap((batchId: string) => {
        if (!batchId) {
          throw new Error('Không nhận được BatchId sau khi enqueue');
        }
        const progress$ = this.pollProgress(batchId, getProgressFn, pollMs);
        return of({ batchId, progress$ } as StartImportResult);
      })
    );
  }
}
