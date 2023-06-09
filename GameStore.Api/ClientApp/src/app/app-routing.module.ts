import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {GameListPageComponent} from "./games/pages/game-list-page/game-list-page.component";
import {GameDetailsPageComponent} from "./games/pages/game-details-page/game-details-page.component";
import {CreateGamePageComponent} from "./games/pages/create-game-page/create-game-page.component";
import {
    PublisherDetailsPageComponent
} from "./publishers/pages/publisher-details-page/publisher-details-page.component";
import {CreatePublisherPageComponent} from "./publishers/pages/create-publisher-page/create-publisher-page.component";
import {
    ShoppingCartDetailsPageComponent
} from "./shopping-carts/pages/shopping-cart-details-page/shopping-cart-details-page.component";
import {MakeOrderPageComponent} from "./orders/pages/make-order-page/make-order-page.component";
import {IboxPaymentPageComponent} from "./orders/pages/ibox-payment-page/ibox-payment-page.component";
import {VisaPaymentPageComponent} from "./orders/pages/visa-payment-page/visa-payment-page.component";
import {BankPaymentPageComponent} from "./orders/pages/bank-payment-page/bank-payment-page.component";
import {BanPageComponent} from "./games/pages/ban-page/ban-page.component";
import {RegistrationPageComponent} from "./auth/pages/registration-page/registration-page.component";
import {LoginPageComponent} from "./auth/pages/login-page/login-page.component";
import {ErrorPageComponent} from "./shared/pages/error-page/error-page.component";
import {LogoutPageComponent} from "./auth/pages/logout-page/logout-page.component";
import {GenreListPageComponent} from "./genres/pages/genre-list-page/genre-list-page.component";
import {UpdatePublisherPageComponent} from "./publishers/pages/update-publisher-page/update-publisher-page.component";
import {PublisherListPageComponent} from "./publishers/pages/publisher-list-page/publisher-list-page.component";
import {GameListManagementComponent} from "./games/components/game-list-management/game-list-management.component";
import {
    GameListManagementPageComponent
} from "./games/pages/game-list-management-page/game-list-management-page.component";
import {UpdateGamePageComponent} from "./games/pages/update-game-page/update-game-page.component";
import {AuthGuard} from "./auth/guards/auth.guard";
import {OrderListPageComponent} from "./orders/pages/order-list-page/order-list-page.component";
import {IsGameAssociatedWithPublisherGuard} from "./publishers/guards/is-game-associated-with-publisher.guard";
import {IsUserAssociatedWithPublisherGuard} from "./publishers/guards/is-user-associated-with-publisher.guard";

const routes: Routes = [
    {path: '', redirectTo: '/game/list/1', pathMatch: 'full'},
    {path: 'game/list/management', redirectTo: 'game/list/management/1'},
    {
        path: 'game/list/management/:page',
        component: GameListManagementPageComponent,
        canActivate: [AuthGuard],
        data: { expectedRoles: ['Manager', 'Moderator', 'Publisher'] }
    },
    {
        path: 'game/create',
        component: CreateGamePageComponent,
        canActivate: [AuthGuard],
        data: { expectedRoles: ['Manager'] }
    },
    {
        path: 'game/update/:key',
        component: UpdateGamePageComponent,
        canActivate: [AuthGuard, IsGameAssociatedWithPublisherGuard],
        data: { expectedRoles: ['Manager', 'Publisher'] }
    },
    {path: 'game/list/:page', component: GameListPageComponent},
    {path: 'game/:key', component: GameDetailsPageComponent},
    {path: 'publisher/list', redirectTo: 'publisher/list/1'},
    {
        path: 'publisher/list/:page',
        component: PublisherListPageComponent,
        canActivate: [AuthGuard],
        data: { expectedRoles: ['Manager'] }
    },
    {
        path: 'publisher/create',
        component: CreatePublisherPageComponent,
        canActivate: [AuthGuard],
        data: { expectedRoles: ['Manager'] }
    },
    {
        path: 'publisher/update/:companyName',
        component: UpdatePublisherPageComponent,
        canActivate: [AuthGuard, IsUserAssociatedWithPublisherGuard],
        data: { expectedRoles: ['Manager', 'Publisher'] }
    },
    {path: 'publisher/:companyName', component: PublisherDetailsPageComponent},
    {path: 'genre/list', redirectTo: 'genre/list/1'},
    {
        path: 'genre/list/:page',
        component: GenreListPageComponent,
        canActivate: [AuthGuard],
        data: {expectedRoles: ['Manager']}
    },
    {path: 'shopping-cart', component: ShoppingCartDetailsPageComponent},
    {path: 'make-order', component: MakeOrderPageComponent},
    {path: 'bank-payment', component: BankPaymentPageComponent},
    {path: 'ibox-payment', component: IboxPaymentPageComponent},
    {path: 'visa-payment', component: VisaPaymentPageComponent},
    {path: 'ban/:commentId', component: BanPageComponent},
    {path: 'admin-panel', redirectTo: '/admin-panel/user/list/1', pathMatch: 'full'},
    {
        path: 'admin-panel',
        loadChildren: () => import('./admin-panel/admin-panel.module').then(m => m.AdminPanelModule),
        canActivate: [AuthGuard],
        data: { expectedRoles: ['Administrator'] }
    },
    {path: 'order/list', redirectTo: 'order/list/1'},
    {path: 'order/list/:page', component: OrderListPageComponent},
    {path: 'register', component: RegistrationPageComponent},
    {path: 'login', component: LoginPageComponent},
    {path: 'logout', component: LogoutPageComponent},
    {path: 'error', component: ErrorPageComponent},
];

@NgModule({
    imports: [RouterModule.forRoot(routes, {anchorScrolling: 'enabled'})],
    exports: [RouterModule]
})
export class AppRoutingModule {
}
