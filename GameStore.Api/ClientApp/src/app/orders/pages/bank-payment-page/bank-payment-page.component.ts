import {Component, OnInit} from '@angular/core';
import {GetOrderResponse} from "../../models/GetOrderResponse";
import {OrderService} from "../../services/order.service";
import {PaymentService} from "../../services/payment.service";
import {ModalService} from "../../../shared/services/modal.service";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-bank-payment-page',
  templateUrl: './bank-payment-page.component.html',
  styleUrls: ['./bank-payment-page.component.scss']
})
export class BankPaymentPageComponent implements OnInit{

    constructor(
        private orderService: OrderService,
        private paymentService: PaymentService,
        private modalService: ModalService,
        private toaster: ToastrService
    ) { }
    ngOnInit(): void {
        this.payByBank();
    }

    private payByBank(): void{
        this.orderService.createOrder().subscribe({
            next: (response: GetOrderResponse) => {
                this.paymentService.payByBank(response.OrderId);
                this.modalService.openInfoModalWithRedirection(
                    "Success!",
                    `The order ${response.OrderId} has been paid successfully!`
                )
            },
            error: (error) =>{
                this.toaster.error(error.error);
            }
        });
    }

}
