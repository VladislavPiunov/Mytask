import { NgModule } from '@angular/core';
import {TasktrackerRoutingModule} from "./tasktracker-routing.module";
import {SharedModule} from "../../shared/shared.module";
import { CommonModule } from '@angular/common';
import { StageColorDirective } from './directives/stage.color.directive';
import { MaterialModule } from 'src/app/core/material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CdkDrag, CdkDropList, CdkDropListGroup } from '@angular/cdk/drag-drop';
import { FilterByStagePipe } from './pipe/filter-by-stage.pipe';
import { FilterByUsersPipe } from './pipe/filter-by-users.pipe';


@NgModule({
  imports: [
    SharedModule,
    CommonModule,
    MaterialModule,
    TasktrackerRoutingModule,
    StageColorDirective,
    FormsModule,
    ReactiveFormsModule,
    CdkDrag,
    CdkDropList,
    CdkDropListGroup
  ],
  declarations: [
    TasktrackerRoutingModule.components,
    FilterByStagePipe,
    FilterByUsersPipe
  ],
  providers: [FilterByStagePipe, FilterByUsersPipe]
})
export class TasktrackerModule { }
