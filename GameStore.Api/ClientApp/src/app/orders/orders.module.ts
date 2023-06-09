import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MakeOrderPageComponent } from './pages/make-order-page/make-order-page.component';
import {SharedModule} from "../shared/shared.module";
import { PaymentMethodsComponent } from './components/payment-methods/payment-methods.component';
import { VisaPaymentPageComponent } from './pages/visa-payment-page/visa-payment-page.component';
import { IboxPaymentPageComponent } from './pages/ibox-payment-page/ibox-payment-page.component';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import { BankPaymentPageComponent } from './pages/bank-payment-page/bank-payment-page.component';
import { OrderListPageComponent } from './pages/order-list-page/order-list-page.component';
import { OrderListComponent } from './components/order-list/order-list.component';
import {RouterLink} from "@angular/router";



@NgModule({
  declarations: [
    MakeOrderPageComponent,
    PaymentMethodsComponent,
    VisaPaymentPageComponent,
    IboxPaymentPageComponent,
    BankPaymentPageComponent,
    OrderListPageComponent,
    OrderListComponent,
  ],
    imports: [
        CommonModule,
        SharedModule,
        FormsModule,
        RouterLink,
        ReactiveFormsModule
    ]
})
export class OrdersModule { }
