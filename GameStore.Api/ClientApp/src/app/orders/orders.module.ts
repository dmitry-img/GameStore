import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MakeOrderPageComponent } from './pages/make-order-page/make-order-page.component';
import {SharedModule} from "../shared/shared.module";
import { PaymentMethodsComponent } from './components/payment-methods/payment-methods.component';
import { IboxPaymentComponent } from './components/ibox-payment/ibox-payment.component';
import { VisaPaymentComponent } from './components/visa-payment/visa-payment.component';
import { VisaPaymentPageComponent } from './pages/visa-payment-page/visa-payment-page.component';
import { IboxPaymentPageComponent } from './pages/ibox-payment-page/ibox-payment-page.component';
import {FormsModule} from "@angular/forms";
import { BankPaymentPageComponent } from './pages/bank-payment-page/bank-payment-page.component';



@NgModule({
  declarations: [
    MakeOrderPageComponent,
    PaymentMethodsComponent,
    IboxPaymentComponent,
    VisaPaymentComponent,
    VisaPaymentPageComponent,
    IboxPaymentPageComponent,
    BankPaymentPageComponent,
  ],
    imports: [
        CommonModule,
        SharedModule,
        FormsModule
    ]
})
export class OrdersModule { }
