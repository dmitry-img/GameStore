import {Component, OnInit} from '@angular/core';
import {GetOrderResponse} from "../../models/GetOrderResponse";
import {OrderService} from "../../services/order.service";
import {PaymentService} from "../../services/payment.service";
import {ModalService} from "../../../shared/services/modal.service";

@Component({
  selector: 'app-ibox-payment',
  templateUrl: './ibox-payment.component.html',
  styleUrls: ['./ibox-payment.component.scss']
})
export class IboxPaymentComponent implements OnInit {
    order!: GetOrderResponse;

    constructor(
        private orderService: OrderService,
        private paymentService: PaymentService,
        private modalService: ModalService
    ) { }
    ngOnInit(): void {
        this.orderService.createOrder().subscribe((response: GetOrderResponse) =>{
            this.order = response;
        })
    }

    onPay() {
        this.paymentService.payByIbox(this.order.OrderId).subscribe(() =>{
            this.modalService.openInfoModalWithRedirection(
                "Success!",
                `The order ${this.order.OrderId} has been paid successfully!`
            );
        });
    }
}
