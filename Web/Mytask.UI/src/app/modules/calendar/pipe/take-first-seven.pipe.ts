import { Pipe, PipeTransform } from "@angular/core";

@Pipe({
    name: 'takeFirstSeven',
    pure: false
})

export class TakeFirstSevenPipe implements PipeTransform {
    transform(items: (Date | null)[], num: number): (Date | null)[] {
       return items.slice(num*7, (num*7)+7);
    }
}