import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {
    ShoppingCartDetailsPageComponent
} from './pages/shopping-cart-details-page/shopping-cart-details-page.component';
import {ShoppingCartDetailsComponent} from './components/shopping-cart-details/shopping-cart-details.component';
import {RouterLink} from "@angular/router";


@NgModule({
    declarations: [
        ShoppingCartDetailsPageComponent,
        ShoppingCartDetailsComponent
    ],
    imports: [
        CommonModule,
        RouterLink
    ]
})
export class ShoppingCartsModule {
}
