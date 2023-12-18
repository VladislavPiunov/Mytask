import { Component, DebugElement } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { StageColorDirective } from './stage.color.directive';

@Component({
    standalone: true,
    template: ` <h2 appStageColor="#1976d2">Something Blue</h2>
      <h2 appStageColor>The Default (purple)</h2>
      <h2>No Color</h2>
      <input #box [appStageColor]="box.value" value="#af19d2" />`,
    imports: [StageColorDirective],
  })
  class TestComponent {}

describe('StageColorDirective', () => {
  let fixture: ComponentFixture<TestComponent>;
  let des: DebugElement[]; // the three elements w/ the directive
  let bareH2: DebugElement;

  beforeEach(() => {
    fixture = TestBed.configureTestingModule({
      imports: [StageColorDirective, TestComponent]
    }).createComponent(TestComponent);

    fixture.detectChanges(); // initial binding

    // all elements with an attached StageColorDirective
    des = fixture.debugElement.queryAll(By.directive(StageColorDirective));

    // the h2 without the StageColorDirective
    bareH2 = fixture.debugElement.query(By.css('h2:not([appStageColor])'));
  });

  // color tests
  it('should have three highlighted elements', () => {
    expect(des.length).toBe(3);
  });

  it('should color 1st <h2> background "#1976d2"', () => {
    const bgColor = des[0].nativeElement.style.backgroundColor;
    expect(bgColor).toBe('rgba(25, 118, 210, 0.5)');
  });

  it('should color 2nd <h2> background w/ default color', () => {
    const bgColor = des[1].nativeElement.style.backgroundColor;
    expect(bgColor).toBe("rgba(191, 185, 255, 0.5)");
  });

  it('should bind <input> background to value color', () => {
    // easier to work with nativeElement
    const input = des[2].nativeElement as HTMLInputElement;
    expect(input.style.backgroundColor)
      .withContext('initial backgroundColor')
      .toBe('rgba(175, 25, 210, 0.5)');

    input.value = '#19d220';

    // Dispatch a DOM event so that Angular responds to the input value change.
    input.dispatchEvent(new Event('input'));
    fixture.detectChanges();

    expect(input.style.backgroundColor)
      .withContext('changed backgroundColor')
      .toBe('rgba(25, 210, 32, 0.5)');
  });

  it('bare <h2> should not have a customProperty', () => {
    expect(bareH2.properties['customProperty']).toBeUndefined();
  });
});
