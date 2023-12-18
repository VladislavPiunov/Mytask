import {NgModule} from '@angular/core';
import {TasktrackerComponent} from "./tasktracker.component";
import {RouterModule, Routes} from "@angular/router";

const routes: Routes = [
  { path: '', component: TasktrackerComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TasktrackerRoutingModule {
  static components: any[] = [
    TasktrackerComponent
  ]
}
