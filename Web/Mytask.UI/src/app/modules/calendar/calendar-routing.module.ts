import { NgModule } from "@angular/core";
import { CalendarComponent } from "./calendar.component";
import { RouterModule, Routes } from "@angular/router";
import { CreateMeetingDialogComponent } from "./dialogs/create-meeting/create-meeting.dialog";
import { EditMeetingDialogComponent } from "./dialogs/edit-meeting/edit-meeting.dialog";

const routes: Routes = [
    { path: '', component: CalendarComponent }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class CalendarRoutingModule {
    static components: any[] = [
        CalendarComponent,
        CreateMeetingDialogComponent,
        EditMeetingDialogComponent
    ]
}