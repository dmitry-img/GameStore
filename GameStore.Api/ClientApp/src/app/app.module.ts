import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {HttpClientModule} from "@angular/common/http";
import {SharedModule} from "./shared/shared.module";
import {GamesModule} from "./games/games.module";
import {PublishersModule} from "./publishers/publishers.module";
import {
    ShoppingCartDetailsPageComponent
} from "./shopping-carts/pages/shopping-cart-details-page/shopping-cart-details-page.component";
import {ShoppingCartsModule} from "./shopping-carts/shopping-carts.module";

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
        ShoppingCartsModule
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule {
}
