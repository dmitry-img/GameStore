import {Component, OnInit} from '@angular/core';
import {OrderService} from "../../services/order.service";
import {PaymentService} from "../../services/payment.service";
import {ModalService} from "../../../shared/services/modal.service";
import {GetOrderResponse} from "../../models/GetOrderResponse";

@Component({
  selector: 'app-ibox-payment-page',
  templateUrl: './ibox-payment-page.component.html',
  styleUrls: ['./ibox-payment-page.component.scss']
})
export class IboxPaymentPageComponent implements OnInit {
    order!: GetOrderResponse;
    constructor(
        private orderService: OrderService,
        private paymentService: PaymentService,
        private modalService: ModalService
    ) { }

    ngOnInit(): void {
        this.getOrder();
    }

    getOrder(): void{
        this.orderService.createOrder().subscribe((order: GetOrderResponse) =>{
            this.order = order;
        })
    }

    onPay(): void {
        this.paymentService.payByIbox(this.order.OrderId).subscribe(() =>{
            this.modalService.openInfoModalWithRedirection(
                "Success!",
                `The order ${this.order.OrderId} has been paid successfully!`
            );
        });
    }
}
