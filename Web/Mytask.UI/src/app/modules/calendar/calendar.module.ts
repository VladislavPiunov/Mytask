import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { MaterialModule } from "src/app/core/material.module";
import { SharedModule } from "src/app/shared/shared.module";
import { CalendarRoutingModule } from "./calendar-routing.module";
import { TakeFirstSevenPipe } from "./pipe/take-first-seven.pipe";
import { FilterByDatePipe } from "./pipe/filter-by-date.pipe";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";

@NgModule({
    imports: [
        SharedModule,
        CommonModule,
        MaterialModule,
        CalendarRoutingModule,
        FormsModule,
        ReactiveFormsModule
    ],
    declarations: [
        CalendarRoutingModule.components,
        TakeFirstSevenPipe,
        FilterByDatePipe,
    ],
    providers: [ TakeFirstSevenPipe, FilterByDatePipe ]
})

export class CalendarModule { }