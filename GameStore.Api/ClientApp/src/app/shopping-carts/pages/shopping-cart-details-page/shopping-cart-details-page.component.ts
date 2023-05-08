import {Component, OnInit} from '@angular/core';
import {ShoppingCartService} from "../../../core/services/shopping-cart.service";
import {GetShoppingCartItemResponse} from "../../models/GetShoppingCartItemResponse";

@Component({
    selector: 'app-shopping-cart-details-page',
    templateUrl: './shopping-cart-details-page.component.html',
    styleUrls: ['./shopping-cart-details-page.component.scss']
})
export class ShoppingCartDetailsPageComponent implements OnInit {
    items!: GetShoppingCartItemResponse[];

    constructor(private shoppingCartService: ShoppingCartService) {
    }

    ngOnInit(): void {
        this.getItems();
    }

    onDeleteItem(gameKey: string): void {
        this.shoppingCartService.deleteItem(gameKey).subscribe(() => {
            this.getItems();
        });
    }

    private getItems(): void {
        this.shoppingCartService.getAllItems().subscribe((items: GetShoppingCartItemResponse[]) => {
            this.items = items;
        })
    }
}
