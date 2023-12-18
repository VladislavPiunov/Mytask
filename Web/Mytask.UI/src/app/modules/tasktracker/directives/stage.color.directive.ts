import { Directive, ElementRef, Input, OnChanges } from '@angular/core';

@Directive({
  standalone: true,
  selector: '[appStageColor]'
})
export class StageColorDirective implements OnChanges {
  defaultColor = "#BFB9FF";

  @Input() appStageColor = "";

  constructor(
    private el: ElementRef  
  ) 
  { }

  ngOnChanges(): void {
    this.setBackgroundColor(this.el);
  }

  private setBackgroundColor (el: ElementRef) {
    el.nativeElement.style.background =  this.addAlpha(this.appStageColor || this.defaultColor, 0.5);
  }

  private addAlpha(color: string, opacity: number): string {
    // coerce values so ti is between 0 and 1.
    const _opacity = Math.round(Math.min(Math.max(opacity || 1, 0), 1) * 255);
    return color + _opacity.toString(16).toUpperCase();
}
}
