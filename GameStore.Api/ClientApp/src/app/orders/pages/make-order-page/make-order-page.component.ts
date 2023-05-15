import { Component } from '@angular/core';
import {GetShoppingCartItemResponse} from "../../../shopping-carts/models/GetShoppingCartItemResponse";
import {ShoppingCartService} from "../../../core/services/shopping-cart.service";
import {GamesTableItem} from "../../../shared/models/GamesTableItem";
import {PaymentMethod} from "../../models/PaymentMethod";

@Component({
  selector: 'app-make-order-page',
  templateUrl: './make-order-page.component.html',
  styleUrls: ['./make-order-page.component.scss']
})
export class MakeOrderPageComponent {
    shoppingCartItems!: GamesTableItem[];
    paymentMethods: PaymentMethod[] = [
        { id: 'bank', name: 'Bank', icon: 'assets/bank-icon.png', description: 'Pay directly from your bank account' },
        { id: 'ibox', name: 'IBox', icon: 'assets/ibox-icon.png', description: 'Pay using an IBox terminal' },
        { id: 'visa', name: 'Visa', icon: 'assets/visa-icon.png', description: 'Pay using your Visa card' },
    ];
    constructor(private shoppingCartService: ShoppingCartService) { }

    ngOnInit(): void {
        this.shoppingCartService.getAllItems().subscribe((items: GamesTableItem[]) => {
            this.shoppingCartItems = items;
        })
    }
}
