import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {HttpClientModule} from "@angular/common/http";
import {SharedModule} from "./shared/shared.module";
import {PublishersModule} from "./publishers/publishers.module";
import {ShoppingCartsModule} from "./shopping-carts/shopping-carts.module";
import {OrdersModule} from "./orders/orders.module";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {GamesModule} from "./games/games.module";
import {AuthModule} from "./auth/auth.module";
import {GenresModule} from "./genres/genres.module";

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
        PublishersModule,
        ShoppingCartsModule,
        OrdersModule,
        BrowserAnimationsModule,
        AuthModule,
        GenresModule
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule {
}
