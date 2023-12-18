import { NgModule } from '@angular/core';
import {TasktrackerRoutingModule} from "./tasktracker-routing.module";
import {SharedModule} from "../../shared/shared.module";
import { CommonModule } from '@angular/common';
import { StageColorDirective } from './directives/stage.color.directive';
import { MaterialModule } from 'src/app/core/material.module';



@NgModule({
  imports: [
    SharedModule,
    CommonModule,
    MaterialModule,
    TasktrackerRoutingModule,
    StageColorDirective
  ],
  declarations: [
    TasktrackerRoutingModule.components
  ]
})
export class TasktrackerModule { }
