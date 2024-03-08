import { Pipe, PipeTransform } from "@angular/core";

@Pipe({
    name: 'addTaskCount',
    pure: false
})
export class AddTaskCountPipe implements PipeTransform {
    transform(value: string, count: number): string {
        return value + "(" + count + ")";
    }
}