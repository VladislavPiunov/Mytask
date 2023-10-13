import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {MaterialModule} from "../core/material.module";
import {RouterModule} from "@angular/router";
import {FormsModule} from "@angular/forms";
import {HeaderComponent} from "../core/layout/header/header.component";
import {DrawerComponent} from "../core/layout/drawer/drawer.component";
import {LayoutComponent} from "../core/layout/layout.component";

@NgModule({
  imports: [
    CommonModule,
    MaterialModule,
    RouterModule,
    FormsModule,
  ],
  declarations: [
    HeaderComponent,
    DrawerComponent,
    LayoutComponent
  ],
  exports: [
    LayoutComponent
  ]
})
export class SharedModule { }