import {Directive, inject} from '@angular/core';
import {AbstractControl, AsyncValidator, NG_ASYNC_VALIDATORS, ValidationErrors} from '@angular/forms';
import {BookService} from '../services/book.service';
import {from, map, Observable} from 'rxjs';

@Directive({
  selector: '[appBookTitleCheck]',
  providers: [
    {
      provide: NG_ASYNC_VALIDATORS,
      useExisting: BockTitleCheck,
      multi: true
    }
  ]
})
export class BockTitleCheck implements AsyncValidator {
  private readonly bookService: BookService = inject(BookService);
  validate(control: AbstractControl): Observable<ValidationErrors | null> {
    const value: string = control.value;
    console.log(value);
    return from(this.bookService.titleExists(value)).pipe(
      map((result: boolean) => (!result ? null : { titleInvalid: true }))
    );
  }
}
