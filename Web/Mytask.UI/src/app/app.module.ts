import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {NotfoundComponent} from "./core/notfound/notfound.component";
import {SharedModule} from "./shared/shared.module";
import { LoginComponent } from './core/auth/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { MaterialModule } from './core/material.module';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import { RUS_DATE_FORMATS, RusDateAdapter } from './core/rus.date.adapter';

@NgModule({
  declarations: [
    AppComponent,
    NotfoundComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MaterialModule,
    SharedModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  exports: [
  ],
  bootstrap: [AppComponent],
  providers: [
    {provide: MAT_DATE_FORMATS, useValue: RUS_DATE_FORMATS},
    {provide: MAT_DATE_LOCALE, useValue: 'ru-RU'},
    {provide: DateAdapter, useClass: RusDateAdapter},
  ]
})
export class AppModule {
}
