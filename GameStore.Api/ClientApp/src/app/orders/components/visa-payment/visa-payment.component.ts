import {Component, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {OrderService} from "../../services/order.service";
import {PaymentService} from "../../services/payment.service";
import {ModalService} from "../../../shared/services/modal.service";
import {ToastrService} from "ngx-toastr";
import {GetOrderResponse} from "../../models/GetOrderResponse";

@Component({
  selector: 'app-visa-payment',
  templateUrl: './visa-payment.component.html',
  styleUrls: ['./visa-payment.component.scss']
})
export class VisaPaymentComponent implements OnInit{
    paymentForm!: FormGroup;

    constructor(
        private orderService: OrderService,
        private paymentService: PaymentService,
        private modalService: ModalService,
        private toaster: ToastrService
    ) { }
    ngOnInit() {
        this.paymentForm = new FormGroup({
            'cardHolderName': new FormControl(null, Validators.required),
            'cardNumber': new FormControl(null, [Validators.required, Validators.pattern(/^\d{4} \d{4} \d{4} \d{4}$/)]),
            'expiryDate': new FormControl(null, [Validators.required, Validators.pattern(/^(0[1-9]|1[0-2])\/\d{2}$/)]),
            'cvv2': new FormControl(null, [Validators.required, Validators.pattern(/^\d{3}$/)])
        });
    }

    onSubmit() {
        if(this.paymentForm.valid){
            this.payByVisa();
        }
    }

    private payByVisa(){
        this.orderService.createOrder().subscribe({
            next: (response: GetOrderResponse) => {
                this.paymentService.payByVisa(response.OrderId).subscribe(() => {
                    this.modalService.openInfoModalWithRedirection(
                        "Success!",
                        `The order ${response.OrderId} has been paid successfully!`
                    );
                });
            },
            error: (error) =>{
                this.toaster.error(error.error);
            }
        });
    }
}
