import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {NotfoundComponent} from "./core/notfound/notfound.component";

const routes: Routes = [
  {path: '', pathMatch: 'full', redirectTo: '/boardName'},
  {
    path: 'boardName',
    loadChildren: () =>
      import('./modules/tasktracker/tasktracker.module').then(
        m => m.TasktrackerModule
      )
  },
  {path: '**', pathMatch: 'full', component: NotfoundComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
