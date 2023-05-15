import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {GameListPageComponent} from "./games/pages/game-list-page/game-list-page.component";
import {GameDetailsPageComponent} from "./games/pages/game-details-page/game-details-page.component";
import {CreateGameComponent} from "./games/components/create-game/create-game.component";
import {CreateGamePageComponent} from "./games/pages/create-game-page/create-game-page.component";
import {
    PublisherDetailsPageComponent
} from "./publishers/pages/publisher-details-page/publisher-details-page.component";
import {CreatePublisherPageComponent} from "./publishers/pages/create-publisher-page/create-publisher-page.component";
import {
    ShoppingCartDetailsComponent
} from "./shopping-carts/components/shopping-cart-details/shopping-cart-details.component";
import {
    ShoppingCartDetailsPageComponent
} from "./shopping-carts/pages/shopping-cart-details-page/shopping-cart-details-page.component";
import {MakeOrderPageComponent} from "./orders/pages/make-order-page/make-order-page.component";
import {IboxPaymentPageComponent} from "./orders/pages/ibox-payment-page/ibox-payment-page.component";
import {VisaPaymentPageComponent} from "./orders/pages/visa-payment-page/visa-payment-page.component";
import {BankPaymentPageComponent} from "./orders/pages/bank-payment-page/bank-payment-page.component";

const routes: Routes = [
    {path: '', redirectTo: '/game/list', pathMatch: 'full'},
    {path: 'game/list', component: GameListPageComponent},
    {path: 'game/create', component: CreateGamePageComponent},
    {path: 'game/:key', component: GameDetailsPageComponent},
    {path: 'publisher/create', component: CreatePublisherPageComponent},
    {path: 'publisher/:companyName', component: PublisherDetailsPageComponent},
    {path: 'shopping-cart', component: ShoppingCartDetailsPageComponent},
    {path: 'make-order', component: MakeOrderPageComponent},
    {path: 'bank-payment', component: BankPaymentPageComponent},
    {path: 'ibox-payment', component: IboxPaymentPageComponent},
    {path: 'visa-payment', component: VisaPaymentPageComponent}
];

@NgModule({
    imports: [RouterModule.forRoot(routes, {anchorScrolling: 'enabled'})],
    exports: [RouterModule]
})
export class AppRoutingModule {
}
