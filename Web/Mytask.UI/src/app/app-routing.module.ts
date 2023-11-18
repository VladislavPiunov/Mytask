import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {NotfoundComponent} from "./core/notfound/notfound.component";
import { LoginComponent } from './core/auth/login.component';

const routes: Routes = [
  {path: '', pathMatch: 'full', redirectTo: '/board'},
  {
    path: 'board',
    loadChildren: () =>
      import('./modules/tasktracker/tasktracker.module').then(
        m => m.TasktrackerModule
      )
  },
  {path: 'login', component: LoginComponent},
  {path: '**', pathMatch: 'full', component: NotfoundComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
