import { Pipe, PipeTransform } from "@angular/core";
import { Meeting } from "../model/meeting.model";

@Pipe({
    name: 'filterByDate',
    pure: false
})

export class FilterByDatePipe implements PipeTransform {
    transform(items: Meeting[], date: Date | null): Meeting[] {
        if (date == null)
            return [];
       return items.filter(x => x.date.toDateString() == date.toDateString());
    }
}