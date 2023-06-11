import {Component, OnInit, ViewChild} from '@angular/core';
import {Router} from "@angular/router";
import {InfoModalComponent} from "../../../shared/components/info-modal/info-modal.component";
import {BsModalRef, BsModalService} from "ngx-bootstrap/modal";
import {ModalService} from "../../../shared/services/modal.service";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {OrderService} from "../../services/order.service";
import {PaymentService} from "../../services/payment.service";
import {ToastrService} from "ngx-toastr";
import {GetOrderResponse} from "../../models/GetOrderResponse";

@Component({
  selector: 'app-visa-payment-page',
  templateUrl: './visa-payment-page.component.html',
  styleUrls: ['./visa-payment-page.component.scss']
})
export class VisaPaymentPageComponent implements OnInit{
    paymentForm!: FormGroup;

    constructor(
        private orderService: OrderService,
        private paymentService: PaymentService,
        private modalService: ModalService,
        private toaster: ToastrService,
        private router: Router
    ) { }

    ngOnInit(): void {
        this.paymentForm = new FormGroup({
            'cardHolderName': new FormControl(null, Validators.required),
            'cardNumber': new FormControl(null, [Validators.required, Validators.pattern(/^\d{4} \d{4} \d{4} \d{4}$/)]),
            'expiryDate': new FormControl(null, [Validators.required, Validators.pattern(/^(0[1-9]|1[0-2])\/\d{2}$/)]),
            'cvv2': new FormControl(null, [Validators.required, Validators.pattern(/^\d{3}$/)])
        });
    }

    onSubmit(): void {
        if(this.paymentForm.valid){
            this.payByVisa();
        }
    }

    private payByVisa(){
        this.orderService.createOrder().subscribe((response: GetOrderResponse) => {
            this.paymentService.payByVisa(response.Id).subscribe(() => {
                this.modalService.openInfoModalWithRedirection(
                    "Success!",
                    `The order ${response.Id} has been paid successfully!`
                );
            });
        });
    }
}
