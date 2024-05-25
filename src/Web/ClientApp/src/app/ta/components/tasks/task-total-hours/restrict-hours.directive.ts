import { Directive, ElementRef, HostListener } from '@angular/core';

@Directive({
  selector: '[appRestrictHours]'
})
export class RestrictHoursDirective {

  constructor(private el: ElementRef) { }

  @HostListener('input', ['$event']) onInputChange(event) {
    const initialValue = this.el.nativeElement.value;
    this.el.nativeElement.value = initialValue.replace(/[^0-6]/g, '');
    if ( initialValue !== this.el.nativeElement.value) {
      event.stopPropagation();
    }
  }

}
