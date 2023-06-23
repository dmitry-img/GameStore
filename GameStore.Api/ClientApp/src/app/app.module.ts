import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {HTTP_INTERCEPTORS, HttpClientModule} from "@angular/common/http";
import {SharedModule} from "./shared/shared.module";
import {PublishersModule} from "./publishers/publishers.module";
import {ShoppingCartsModule} from "./shopping-carts/shopping-carts.module";
import {OrdersModule} from "./orders/orders.module";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {GamesModule} from "./games/games.module";
import {AuthModule} from "./auth/auth.module";
import {GenresModule} from "./genres/genres.module";
import {HttpErrorInterceptor} from "./core/interceptors/http-error.interceptor";
import {NgxSpinnerModule} from "ngx-spinner";
import {LoaderInterceptor} from "./core/interceptors/loader.interceptor";

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
        GenresModule,
        NgxSpinnerModule
    ],
    providers: [
        { provide: HTTP_INTERCEPTORS, useClass: LoaderInterceptor, multi: true },
        { provide: HTTP_INTERCEPTORS, useClass: HttpErrorInterceptor, multi: true }
    ],
    bootstrap: [AppComponent]
})
export class AppModule {
}
