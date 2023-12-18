import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {MaterialModule} from "../core/material.module";
import {RouterModule} from "@angular/router";
import {FormsModule} from "@angular/forms";
import {HeaderComponent} from "../core/layout/components/header/header.component";
import {DrawerComponent} from "../core/layout/components/drawer/drawer.component";
import {LayoutComponent} from "../core/layout/layout.component";
import { AuthService } from '../core/auth/auth.service';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from '../core/auth/auth.interceptor';

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
  ],
  providers: [
    AuthService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ]
})
export class SharedModule { }