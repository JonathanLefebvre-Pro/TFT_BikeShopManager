import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'listFieldEmptyPlaceholder',
})
export class ListFieldEmptyPlaceholderPipe implements PipeTransform {
  transform(value: string | null): string {
    if(value)
      return value;
    return '-';
  }
}
