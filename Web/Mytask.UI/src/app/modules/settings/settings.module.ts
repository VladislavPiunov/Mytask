import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { MaterialModule } from "src/app/core/material.module";
import { SharedModule } from "src/app/shared/shared.module";
import { SettingsRoutingModule } from "./settings-routing.module";

@NgModule({
    imports: [
      SharedModule,
      CommonModule,
      MaterialModule,
      SettingsRoutingModule
    ],
    declarations: [
        SettingsRoutingModule.components
    ],
    providers: [ ]
  })
  export class SettingsModule { }