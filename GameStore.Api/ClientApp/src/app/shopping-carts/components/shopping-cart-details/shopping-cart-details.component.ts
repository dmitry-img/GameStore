import {Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges} from '@angular/core';
import {GetShoppingCartItemResponse} from "../../models/GetShoppingCartItemResponse";

@Component({
  selector: 'app-shopping-cart-details',
  templateUrl: './shopping-cart-details.component.html',
  styleUrls: ['./shopping-cart-details.component.scss']
})
export class ShoppingCartDetailsComponent implements OnInit, OnChanges {
  @Input() items!: GetShoppingCartItemResponse[];
  @Output() deleteItem = new EventEmitter<string>();
  totalPrice: number = 0;

  ngOnInit(): void {
    this.getTotalPrice()
  }

  ngOnChanges(): void {
    this.getTotalPrice()
  }

  onDelete(gameKey: string): void {
    this.deleteItem.emit(gameKey);
  }

  private getTotalPrice(): void{
    this.totalPrice = 0;
    this.items.forEach(i => this.totalPrice += i.GamePrice * i.Quantity);
  }
}
