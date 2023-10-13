import { NgModule } from '@angular/core';
import {TasktrackerRoutingModule} from "./tasktracker-routing.module";
import {SharedModule} from "../../shared/shared.module";



@NgModule({
  imports: [
    SharedModule,
    TasktrackerRoutingModule,
  ],
  declarations: [
    TasktrackerRoutingModule.components
  ]
})
export class TasktrackerModule { }
