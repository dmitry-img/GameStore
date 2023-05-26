import {Component, OnInit} from '@angular/core';
import {OrderService} from "../../services/order.service";
import {PaymentService} from "../../services/payment.service";
import {ModalService} from "../../../shared/services/modal.service";
import {GetOrderResponse} from "../../models/GetOrderResponse";
import {ToastrService} from "ngx-toastr";
import {Router} from "@angular/router";

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
        private modalService: ModalService,
        private toaster: ToastrService,
        private router: Router
    ) { }

    ngOnInit(): void {
        this.getOrder();
    }

    getOrder(): void{
        this.orderService.createOrder().subscribe({
            next: (order: GetOrderResponse) =>{
                this.order = order;
            },
            error: (err) =>{
                this.toaster.error(err.error);
                this.router.navigate(['/shopping-cart']);
            }
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
