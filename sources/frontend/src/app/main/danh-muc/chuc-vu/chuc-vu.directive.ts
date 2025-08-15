import { Directive, Input } from '@angular/core';
import { NG_ASYNC_VALIDATORS, AsyncValidator, AbstractControl, ValidationErrors } from '@angular/forms';
import { Observable, timer, of } from 'rxjs';
import { switchMap, map, first } from 'rxjs/operators';
import { ChucVuService } from '@app/proxy';
@Directive({
  selector: '[mustUniqueChucVuMa]',
  providers: [
    { provide: NG_ASYNC_VALIDATORS, useExisting: MustUniqueChucVuMaDirective, multi: true },
  ],
  standalone: false,
})
export class MustUniqueChucVuMaDirective implements AsyncValidator {
  @Input('mustUniqueChucVuMa') compareValue: string;

  constructor(private _chucVusServiceProxy: ChucVuService) { }

  validate(control: AbstractControl): Observable<ValidationErrors | null> {
    if (control.value) {
      return timer(500).pipe(
        switchMap(() => this._chucVusServiceProxy.isMaChucVuUnique(control.value)),
        map(res => {
          if (this.compareValue && this.compareValue === control.value.trim()) {
            return null;
          } else {
            return res ? { isValidMustUniqueMa: !res } : null;
          }
        }),
        first(),
      );
    } else {
      return of(null);
    }
  }
}
