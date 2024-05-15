import { Pipe, PipeTransform } from "@angular/core";
import { User } from "src/app/core/model/user.model";

@Pipe({
    name: 'filterByUsers',
    pure: false
})
export class FilterByUsersPipe implements PipeTransform {
    transform(items: User[], users: string[]): any {
        return items.filter(item => users.includes(item.id));
    }
}