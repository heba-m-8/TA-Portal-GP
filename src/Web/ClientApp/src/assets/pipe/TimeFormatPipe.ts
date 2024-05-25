import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'timeFormat'
})
export class TimeFormatPipe implements PipeTransform {
  transform(value: string): string {
    // Assuming value is in the format 'HH:mm:ss'
    const parts = value.split(':');
    if (parts.length === 3) {
      return `${parts[0]}:${parts[1]}`;
    }
    // Handle other cases here if needed
    return value;
  }
}