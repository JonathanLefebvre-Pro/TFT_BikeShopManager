import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'emptyStringPlaceholder',
})
export class EmptyStringPlaceholderPipe implements PipeTransform {
  transform(value: string | null | undefined): string {
    if(value)
      return value;
    return '-';
  }
}
