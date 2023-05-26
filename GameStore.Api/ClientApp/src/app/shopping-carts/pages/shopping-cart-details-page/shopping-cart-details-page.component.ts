import {Component, OnInit} from '@angular/core';
import {ShoppingCartService} from "../../services/shopping-cart.service";
import {GamesTableItem} from "../../../shared/models/GamesTableItem";

@Component({
    selector: 'app-shopping-cart-details-page',
    templateUrl: './shopping-cart-details-page.component.html',
    styleUrls: ['./shopping-cart-details-page.component.scss']
})
export class ShoppingCartDetailsPageComponent implements OnInit {
    items!: GamesTableItem[];

    constructor(private shoppingCartService: ShoppingCartService) { }

    ngOnInit(): void {
        this.getItems();
    }

    onDeleteItem(gameKey: string): void {
        this.shoppingCartService.deleteItem(gameKey).subscribe(() => {
            this.getItems();
        });
    }

    private getItems(): void {
        this.shoppingCartService.getAllItems().subscribe((items: GamesTableItem[]) => {
            this.items = items;
        })
    }
}
