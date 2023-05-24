import {Component, Input} from '@angular/core';
import {Router} from "@angular/router";
import {PaymentMethod} from "../../models/PaymentMethod";
import {OrderService} from "../../services/order.service";
import {PaymentService} from "../../services/payment.service";
import {ToastrService} from "ngx-toastr";
import {ModalService} from "../../../shared/services/modal.service";
import {GetOrderResponse} from "../../models/GetOrderResponse";

@Component({
  selector: 'app-payment-methods',
  templateUrl: './payment-methods.component.html',
  styleUrls: ['./payment-methods.component.scss']
})
export class PaymentMethodsComponent {
    @Input() paymentMethods!: PaymentMethod[];

    constructor(private router: Router) { }

    selectPaymentMethod(method: string): void {
        switch (method) {
            case 'bank':
                this.router.navigate(['/bank-payment'])
                break;
            case 'ibox':
                this.router.navigate(['/ibox-payment']);
                break;
            case 'visa':
                this.router.navigate(['/visa-payment']);
                break;
        }
    }
}
