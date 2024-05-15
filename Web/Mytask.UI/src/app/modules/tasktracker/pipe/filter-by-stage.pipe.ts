import { Pipe, PipeTransform } from "@angular/core";
import { Task } from "../models/task.model";

@Pipe({
    name: 'filterByStage',
    pure: false
})
export class FilterByStagePipe implements PipeTransform {
    transform(items: Task[], stageId: string): any {
        return items.filter(item => item.stageId == stageId);
    }
}