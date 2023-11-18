import { NgModule } from '@angular/core';
import {TasktrackerRoutingModule} from "./tasktracker-routing.module";
import {SharedModule} from "../../shared/shared.module";
import { AuthService } from 'src/app/core/auth/auth.service';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from 'src/app/core/auth/auth.interceptor';
import { CommonModule } from '@angular/common';



@NgModule({
  imports: [
    SharedModule,
    CommonModule,
    TasktrackerRoutingModule,
  ],
  declarations: [
    TasktrackerRoutingModule.components
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
export class TasktrackerModule { }
