import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {HttpClientModule} from "@angular/common/http";
import {SharedModule} from "./shared/shared.module";
import {GamesModule} from "./games/games.module";
import {PublishersModule} from "./publishers/publishers.module";

@NgModule({
  declarations: [
    AppComponent
  ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        HttpClientModule,
        SharedModule,
        GamesModule,
        PublishersModule
    ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
