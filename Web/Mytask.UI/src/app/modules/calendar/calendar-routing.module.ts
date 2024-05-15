import { NgModule } from "@angular/core";
import { CalendarComponent } from "./calendar.component";
import { RouterModule, Routes } from "@angular/router";

const routes: Routes = [
    { path: '', component: CalendarComponent }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class CalendarRoutingModule {
    static components: any[] = [
        CalendarComponent
    ]
}