import {NgModule} from '@angular/core';
import {TasktrackerComponent} from "./tasktracker.component";
import {RouterModule, Routes} from "@angular/router";
import { CreateTaskDialogComponent } from './dialogs/create-task/create-task.dialog';
import { EditTaskDialogComponent } from './dialogs/edit-task/edit-task.dialog';
import { DeleteTaskDialogComponent } from './dialogs/delete-task/delete-task.dialog';

const routes: Routes = [
  { path: '', component: TasktrackerComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TasktrackerRoutingModule {
  static components: any[] = [
    TasktrackerComponent,
    CreateTaskDialogComponent,
    EditTaskDialogComponent,
    DeleteTaskDialogComponent
  ]
}
