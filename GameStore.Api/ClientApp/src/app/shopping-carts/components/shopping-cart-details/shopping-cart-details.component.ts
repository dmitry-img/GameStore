import {Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges} from '@angular/core';
import {GetShoppingCartItemResponse} from "../../models/GetShoppingCartItemResponse";
import {GamesTableItem} from "../../../shared/models/GamesTableItem";

@Component({
    selector: 'app-shopping-cart-details',
    templateUrl: './shopping-cart-details.component.html',
    styleUrls: ['./shopping-cart-details.component.scss']
})
export class ShoppingCartDetailsComponent{
    @Input() items!: GamesTableItem[];
    @Output() deleteItem = new EventEmitter<string>();

    onDelete(gameKey: string): void {
        this.deleteItem.emit(gameKey);
    }
}
