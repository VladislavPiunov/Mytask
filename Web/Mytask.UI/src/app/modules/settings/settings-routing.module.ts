import {NgModule} from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SettingsComponent } from './settings.component';
import { AddUsersDialogComponent } from './dialogs/add-users/add-users.dialog';

const routes: Routes = [
  { path: '', component: SettingsComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SettingsRoutingModule {
  static components: any[] = [
    SettingsComponent,
    AddUsersDialogComponent
  ]
}
